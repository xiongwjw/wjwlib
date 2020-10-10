using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using wjw.socket.Server;
using wjw.socket.Common;

namespace wjw.socket.Busniness
{
    public class FileServer 
    {
        #region field
        private TcpServer _tcpServer;
        private Dictionary<int, FileDataContainer> _queue;
        private uint _headerFlag;
        private bool _override = false;
        private Thread _recievedThread = null;
        private ConcurrentQueue<ReceviedFile> _receivedQueue = new ConcurrentQueue<ReceviedFile>();
        #endregion

        #region event
        public event Action<int> OnAccept;
        public event Action<int, string> OnReceiveFile;
        public event Action<string> OnSendFile;
        public event Action<int, int> OnSend;
        public event Action<int> OnClose;
        public event Action<string> OnError;
        #endregion

        #region public
        public void Close()
        {
            _tcpServer.Close();
        }
        public FileServer(uint headerFlag = 0xff,bool isOverride =false)
        {
            if (headerFlag < 0 || headerFlag > 1023)
            {
                headerFlag = 0;
            }
            _override = isOverride;
            this._headerFlag = headerFlag;
            Thread thread = new Thread(new ThreadStart(() =>
            {
                _queue = new Dictionary<int, FileDataContainer>();
                _tcpServer = new TcpServer(100, 1024, 0); // default value
                _tcpServer.OnAccept += TcpServer_eventactionAccept;
                _tcpServer.OnReceive += TcpServer_eventactionReceive;
                _tcpServer.OnSend += TcpServer_OnSend;
                _tcpServer.OnClose += TcpServer_eventClose;
            }));
            thread.IsBackground = true;
            thread.Start();

            _recievedThread = new Thread(HandleMessageThread);
            _recievedThread.IsBackground = true;
            _recievedThread.Start();

        }
        public bool SendFile(int connectId, string filePath,string destPath="")
        {
            byte[] data = AddHead(filePath,destPath);
            if (data == null)
                return false;
            _tcpServer.Send(connectId, data, 0, data.Length);
            OnSendFile?.Invoke($"connectid:{connectId.ToString()},filepath:{filePath}");
            return true;
        }
        public bool SendFile(string ip, string filePath,string destPath = "")
        {
            byte[] data = AddHead(filePath,destPath);
            if (data == null)
                return false;
            _tcpServer.Send(ip, data, 0, data.Length);
            OnSendFile?.Invoke($"ip:{ip},filepath:{filePath}");
            return true;
        }
        public bool SendFileToAll(string filePath, string destPath = "")
        {
            byte[] data = AddHead(filePath,destPath);
            if (data == null)
                return false;
            _tcpServer.SendToAllClient(data, 0, data.Length);
            OnSendFile?.Invoke($"client:allClient,filepath:{filePath}");
            return true;
        }
        public void Start(int port)
        {
            while (_tcpServer == null)
            {
                Thread.Sleep(10);
            }
            _tcpServer.Start(port);
        }
        public void Close(int connectId)
        {
            _tcpServer.Close(connectId);
        }
        public bool SetAttached(int connectId, object data)
        {
            return _tcpServer.SetAttached(connectId, data);
        }
        public T GetAttached<T>(int connectId)
        {
            return _tcpServer.GetAttached<T>(connectId);
        }
        #endregion

        #region private
        private byte[] AddHead(string fileName,string destFolder)
        {
            //header+filepathLenght+lenght+filepath+data

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
        private byte[] Read(int connectId, out bool isContinueGetData)
        {
            if (!_queue.ContainsKey(connectId))
            {
                isContinueGetData = true;
                return null;
            }


            var dc = _queue[connectId];

            if (dc.Data.Count <= 12)
            {
                isContinueGetData = true;
                return null;
            }


            if (dc.Length == 0)//first time, parse len
            {
                uint checkHeaderFlag = BitConverter.ToUInt32(dc.Data.Take(4).ToArray(), 0);
                if (checkHeaderFlag != _headerFlag)
                {
                    //if the format is not right , then remove all data and again
                    OnError?.Invoke("received headflag not right,so return");
                    dc.Data.Clear();
                    dc.Length = 0;
                    dc.FilePath = string.Empty;
                    _queue[connectId] = dc;
                    isContinueGetData = true;
                    return null;
                }

                dc.FilePathLenght = BitConverter.ToUInt32(dc.Data.Skip(4).Take(4).ToArray(), 0);
                dc.Length = BitConverter.ToUInt32(dc.Data.Skip(8).Take(4).ToArray(), 0);
                dc.FilePath = Encoding.Unicode.GetString(dc.Data.Skip(12).Take((int)dc.FilePathLenght).ToArray());
                _queue[connectId] = dc;//set back to dictionary
            }

            if (dc.Length > dc.Data.Count - 12 - dc.FilePathLenght)
            {
                isContinueGetData = true;
                return null;
            }
            byte[] f = dc.Data.Skip(12 + (int)dc.FilePathLenght).Take((int)dc.Length).ToArray();
            dc.Data.RemoveRange(0, 12+(int)dc.FilePathLenght+(int)dc.Length);
            isContinueGetData = false;
            return f;
        }
        private void TcpServer_eventClose(int connectId)
        {
            if (_queue.ContainsKey(connectId))
            {
                _queue.Remove(connectId);
            }
            if (OnClose != null)
                OnClose(connectId);
        }
        private void TcpServer_eventactionAccept(int connectId)
        {
            if (OnAccept != null)
                OnAccept(connectId);
        }
        private void TcpServer_OnSend(int connectId, int length)
        {
            if (OnSend != null)
            {
                OnSend(connectId, length);
            }
        }
        private void TcpServer_eventactionReceive(int connectId, byte[] data, int offset, int length)
        {
            if (!_queue.ContainsKey(connectId))
            {
                FileDataContainer container = new FileDataContainer { Length = 0, Data = new List<byte>(), FilePathLenght = 0, FilePath = string.Empty };
                _queue.Add(connectId, container);
            }

            byte[] r = new byte[length];
            Buffer.BlockCopy(data, offset, r, 0, length);
            _queue[connectId].Data.AddRange(r);

            bool isContinueGetData = false;
            while (_queue[connectId].Data.Count > 12 && !isContinueGetData)
            {
                byte[] datas = Read(connectId, out isContinueGetData);
                if (datas != null && datas.Length > 0)
                {
                    if (!File.Exists(_queue[connectId].FilePath) || _override)
                    {

                        OnReceiveFile?.Invoke(connectId, _queue[connectId].FilePath);
                        _receivedQueue.Enqueue(new ReceviedFile { Data = datas, FilePath = _queue[connectId].FilePath });

                        //if (FileHelper.WriteFile(datas, queue[connectId].FilePath))
                        //{
                        //    OnReceiveFile?.Invoke(connectId, queue[connectId].FilePath);
                        //}
                        //else
                        //    OnError?.Invoke($"Received file success, but write file failed:{queue[connectId].FilePath}");
                    }
                    else
                        OnError?.Invoke($"Received file success, but file exist:{_queue[connectId].FilePath}");
                    _queue[connectId].InitData();
                }
            }




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
        #endregion

    }
}

