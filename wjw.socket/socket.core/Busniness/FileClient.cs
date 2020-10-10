using wjw.socket.Client;
using wjw.socket.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace wjw.socket.Busniness
{
    public class FileClient
    {
        #region field
        private TcpClients _tcpClients;
        private FileDataContainer _queue;
        private uint _headerFlag;
        private bool _override = false;
        private Thread _recievedThread = null;
        private AutoResetEvent _synSignal = new AutoResetEvent(false);
        private int _synSendLength = 0;
        private ConcurrentQueue<ReceviedFile> _receivedQueue = new ConcurrentQueue<ReceviedFile>();
        #endregion

        #region event
        public event Action<bool> OnConnect;
        public event Action<string> OnReceiveFile;
        public event Action<string> OnSendFile;
        public event Action<int> OnSend;
        public event Action OnClose;
        public event Action<string> OnError;
        public bool Connected
        {
            get
            {
                if (_tcpClients == null)
                {
                    return false;
                }
                return _tcpClients.Connected;
            }
        }
        #endregion

        #region public
        public FileClient(uint headerFlag = 0xff, bool isOverride = false)
        {
            _override = isOverride;
            if (headerFlag < 0 || headerFlag > 1023)
            {
                headerFlag = 0;
            }
            this._headerFlag = headerFlag;
            Thread thread = new Thread(new ThreadStart(() =>
            {
                _queue = new FileDataContainer();
                _tcpClients = new TcpClients(1024);
                _tcpClients.OnConnect += TCPClients_OnConnect;
                _tcpClients.OnReceive += TCPClients_OnReceive;
                _tcpClients.OnClose += TCPClients_OnClose;
                _tcpClients.OnSend += TCPClients_OnSend;
                
            }));
            thread.IsBackground = true;
            thread.Start();
            _recievedThread = new Thread(HandleMessageThread);
            _recievedThread.IsBackground = true;
            _recievedThread.Start();

        }
        public void Close()
        {
            _tcpClients.Close();
        }
        public void Connect(string ip, int port)
        {
            while (_tcpClients == null)
            {
                Thread.Sleep(10);
            }
            _tcpClients.Connect(ip, port);
        }
        public int SendFile(string fileName,string destFolder = "")
        {
            if (Connected)
            {
                byte[] data = AddHead(fileName,destFolder);
                if (data != null)
                {
                    _tcpClients.Send(data, 0, data.Length);
                    OnSendFile?.Invoke(fileName);
                    return data.Length;
                }
                else
                    OnError?.Invoke($"Read file error {fileName}");
            }
            return 0;
        }
        public int SendFileSyn(string fileName, string destFolder = "")
        {
            _synSendLength = SendFile(fileName, destFolder);
            if(Wait())
            {
                OnSendFile?.Invoke(fileName);
                return _synSendLength;
            }
            else
            {
                OnError?.Invoke($"SendFileSyn error {fileName}");
            }
            return 0;
        }
        #endregion

        #region private 
        private bool Wait()
        {
            _synSignal.Reset();
            if (_synSignal.WaitOne(1800000))//wait 30 min will timeout
                return true;
            else
                return false;
        }
        private void TCPClients_OnConnect(bool success)
        {
            if (OnConnect != null)
                OnConnect(success);
        }
        private void TCPClients_OnSend(int length)
        {
            if (OnSend != null)
                OnSend(length);
            if (length == _synSendLength)
                _synSignal.Set();
        }
        private void TCPClients_OnReceive(byte[] data, int offset, int length)
        {
            byte[] r = new byte[length];
            Buffer.BlockCopy(data, offset, r, 0, length);
            _queue.Data.AddRange(r);

            bool isContinueGetData = false;
            while (_queue.Data.Count > 12 && !isContinueGetData)
            {
                byte[] datas = Read(out isContinueGetData);
                if (datas != null && datas.Length > 0)
                {
                    if (!File.Exists(_queue.FilePath) || _override)
                    {
                        OnReceiveFile?.Invoke(_queue.FilePath); // invoke the message first. then write file
                        _receivedQueue.Enqueue(new ReceviedFile { Data = datas, FilePath = _queue.FilePath });
                    }
                    else
                        OnError?.Invoke($"Received file success, but file exist:{_queue.FilePath}");

                    _queue.InitData();
                }
            }


        }
        private void TCPClients_OnClose()
        {
            _queue.InitData();
            if (OnClose != null)
                OnClose();
        }
        private void HandleMessageThread()
        {
            while (true)
            {
                ReceviedFile file = null;
                while (_receivedQueue.TryDequeue(out file))
                {
                    try
                    {
                        string folder = Path.GetDirectoryName(file.FilePath);
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder);
                        if (FileHelper.WriteFile(file.Data, file.FilePath))
                        {
                           // OnReceiveFile?.Invoke(queue.FilePath);
                        }
                        else
                            OnError?.Invoke($"Received file success, but write file failed:{file.FilePath}");
                    }
                    catch (System.Exception ex)
                    {
                        OnError?.Invoke(ex.Message);
                    }
                }
                Thread.Sleep(1000);
            }
        }
        private byte[] AddHead(string fileName,string destFolder)
        {
            byte[] data = FileHelper.ReadFile(fileName);
            if (data == null)
                return null;
            if (destFolder != string.Empty && FileHelper.IsValidatePath(destFolder))
                fileName = FileHelper.ReplaceDirectory(fileName, destFolder);
                
            byte[] filePathArray = Encoding.Unicode.GetBytes(fileName);
            uint filePathLength = (uint)filePathArray.Length;
            uint len = (uint)data.Length;
            return System.BitConverter.GetBytes(_headerFlag)
                .Concat(System.BitConverter.GetBytes(filePathLength))
                .Concat(System.BitConverter.GetBytes(len))
                .Concat(filePathArray)
                .Concat(data).ToArray();
        }
        private byte[] Read(out bool isContinueGetData )
        {
            if (_queue.Data.Count <= 12)
            {
                isContinueGetData = true;
                return null;
            }

            if (_queue.Length == 0)//first time, parse len
            {
                
                uint checkHeaderFlag = BitConverter.ToUInt32(_queue.Data.Take(4).ToArray(), 0);
                if (checkHeaderFlag != _headerFlag)
                {
                    _queue.Data.Clear();
                    isContinueGetData = true;
                    return null;
                }


                _queue.FilePathLenght = BitConverter.ToUInt32(_queue.Data.Skip(4).Take(4).ToArray(), 0);
                _queue.Length = BitConverter.ToUInt32(_queue.Data.Skip(8).Take(4).ToArray(), 0);
              //  OnError?.Invoke($"file length:{queue.Length}");
                _queue.FilePath = Encoding.Unicode.GetString(_queue.Data.Skip(12).Take((int)_queue.FilePathLenght).ToArray());
            }

            if (_queue.Length > _queue.Data.Count - 12 - _queue.FilePathLenght)
            {
                isContinueGetData = true;
                return null;
            }
            byte[] f = _queue.Data.Skip(12 + (int)_queue.FilePathLenght).Take((int)_queue.Length).ToArray();
            _queue.Data.RemoveRange(0, 12 + (int)_queue.FilePathLenght + (int)_queue.Length);
            isContinueGetData = false;
            return f;
        }
        #endregion

    }

    internal class ReceviedFile
    {
        public byte[] Data { get; set; } = null;
        public string FilePath { get; set; }
    }
}
