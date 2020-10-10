using wjw.socket.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace wjw.socket.Client
{
    public class TcpPackClient
    {
        #region field
        private TcpClients tcpClients;
        private List<byte> queue;
        private uint headerFlag;
        #endregion

        #region event
        public event Action<bool> OnConnect;
        public event Action<byte[]> OnReceive;
        public event Action<int> OnSend;
        public event Action<string> OnError;
        public event Action OnClose;
        public bool Connected
        {
            get
            {
                if (tcpClients == null)
                {
                    return false;
                }
                return tcpClients.Connected;
            }
        }
        #endregion

        #region public
        public TcpPackClient(int receiveBufferSize, uint headerFlag)
        {
            //headerflag from 0-1023, when flag set to 0, don't check header
            if (headerFlag < 0 || headerFlag > 1023)
            {
                headerFlag = 0;
            }
            this.headerFlag = headerFlag;
            Thread thread = new Thread(new ThreadStart(() =>
            {
                queue = new List<byte>();
                tcpClients = new TcpClients(receiveBufferSize);
                tcpClients.OnConnect += TcpClients_OnConnect;
                tcpClients.OnReceive += TcpClients_OnReceive;
                tcpClients.OnClose += TcpClients_OnClose;
                tcpClients.OnSend += TcpClients_OnSend;
                tcpClients.OnError += TcpClients_OnError;
            }));
            thread.IsBackground = true;
            thread.Start();
        }
        public void Send(byte[] data, int offset, int length)
        {
            data = AddHead(data.Skip(offset).Take(length).ToArray());
            tcpClients.Send(data, 0, data.Length);
        }
        public void Connect(string ip, int port)
        {
            while (tcpClients == null)
            {
                Thread.Sleep(10);
            }
            tcpClients.Connect(ip, port);
        }
        public void Close()
        {
            tcpClients.Close();
        }
        #endregion

        #region private
        private void TcpClients_OnSend(int length)
        {
            if (OnSend != null)
                OnSend(length);
        }
        private void TcpClients_OnReceive(byte[] data, int offset, int length)
        {
            byte[] r = new byte[length];
            Buffer.BlockCopy(data, offset, r, 0, length);
            queue.AddRange(r);
            bool isContinueGetData = false;
            while (queue.Count>8 && !isContinueGetData)
            {
                byte[] datas = Read(out isContinueGetData);
                if (datas != null && datas.Length > 0)
                {
                    OnReceive?.Invoke(datas);
                }
            }     
        }
        private void TcpClients_OnConnect(bool success)
        {
            if (OnConnect != null)
                OnConnect(success);
        }
        private void TcpClients_OnClose()
        {
            queue.Clear();
            if (OnClose != null)
                OnClose();
        }
        private void TcpClients_OnError(string msg)
        {
            OnError?.Invoke(msg);
        }
        private byte[] AddHead(byte[] data)
        {
            uint len = (uint)data.Length;
            return System.BitConverter.GetBytes(headerFlag).Concat(System.BitConverter.GetBytes(len)).Concat(data).ToArray();
        }
        private byte[] Read(out bool isContinueGetData)
        {
            if (queue.Count <= 8)
            {
                isContinueGetData = true;
                return null;
            }

            uint checkHeaderFlag = BitConverter.ToUInt32(queue.Take(4).ToArray(), 0);
            uint len = BitConverter.ToUInt32(queue.Skip(4).Take(4).ToArray(), 0);
            if (headerFlag != checkHeaderFlag)
            {
                queue.Clear();
                isContinueGetData = true;
                return null;
            }

            if (len > queue.Count - 8)
            {
                isContinueGetData = true;
                return null;
            }

            byte[] f = queue.Skip(8).Take((int)len).ToArray();
            queue.RemoveRange(0, 8 + (int)len);
            isContinueGetData = false;
            return f;
        }
        #endregion

    }
}
