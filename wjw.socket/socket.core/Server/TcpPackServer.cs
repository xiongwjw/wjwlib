using wjw.socket.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace wjw.socket.Server
{

    public class TcpPackServer
    {
        #region field
        private TcpServer tcpServer;
        private Dictionary<int, DataContainer> queue;
        private uint headerFlag;
        #endregion

        #region event
        public event Action<int> OnAccept;
        public event Action<int, byte[]> OnReceive;
        public event Action<int, int> OnSend;
        public event Action<int> OnClose;
        public event Action<string> OnError;
        public ConcurrentDictionary<int, string> ClientList
        {
            get
            {
                if (tcpServer != null)
                {
                    return tcpServer.clientList;
                }
                else
                {
                    return new ConcurrentDictionary<int, string>();
                }
            }
        }
        #endregion

        #region public
        public void Start(int port)
        {
            while (tcpServer == null)
            {
                Thread.Sleep(10);
            }
            tcpServer.Start(port);
        }
        public bool IsClientConnect(string ip)
        {
            return tcpServer.IsClientConnect(ip);
        }
        public bool SetAttached(int connectId, object data)
        {
            return tcpServer.SetAttached(connectId, data);
        }
        public T GetAttached<T>(int connectId)
        {
            return tcpServer.GetAttached<T>(connectId);
        }
        public void Close(int connectId)
        {
            tcpServer.Close(connectId);
        }
        #endregion

        #region internal
        public TcpPackServer(int numConnections, int receiveBufferSize, int overtime, uint headerFlag)
        {
            //overtime =0 , don't check overtime
            //headerflag =0 , don't check header
            if (headerFlag < 0 || headerFlag > 1023)
            {
                headerFlag = 0;
            }
            this.headerFlag = headerFlag;
            Thread thread = new Thread(new ThreadStart(() =>
            {
                queue = new Dictionary<int, DataContainer>();
                tcpServer = new TcpServer(numConnections, receiveBufferSize, overtime);
                tcpServer.OnAccept += TcpServer_OnAccept;
                tcpServer.OnReceive += TcpServer_OnReceive;
                tcpServer.OnSend += TcpServer_OnSend;
                tcpServer.OnClose += TcpServer_OnClose;
                tcpServer.OnError += TcpServer_OnError;
            }));
            thread.IsBackground = true;
            thread.Start();
        }
        public string GetClientIpById(int connectID)
        {
            return tcpServer.GetClientIpById(connectID);
        }

        public string GetEndPoint(int connectID)
        {
            return tcpServer.GetEndpoint(connectID);
        }

        public int GetClientPortIpById(int connectID)
        {
            return tcpServer.GetClientPortById(connectID);
        }
        public void Send(int connectId, byte[] data, int offset, int length)
        {
            data = AddHead(data.Skip(offset).Take(length).ToArray());
            tcpServer.Send(connectId, data, 0, data.Length);
        }
        public void Send(string ip, byte[] data, int offset, int length)
        {
            data = AddHead(data.Skip(offset).Take(length).ToArray());
            tcpServer.Send(ip, data, 0, data.Length);
        }
        public void SendToAllClient(byte[] data, int offset, int length)
        {
            data = AddHead(data.Skip(offset).Take(length).ToArray());
            tcpServer.SendToAllClient(data, 0, data.Length);
        }
        #endregion

        #region private
        private void TcpServer_OnError(string msg)
        {
            OnError?.Invoke(msg);
        }
        private void TcpServer_OnSend(int connectId, int length)
        {
            if (OnSend != null)
            {
                OnSend(connectId, length);
            }
        }
        private void TcpServer_OnReceive(int connectId, byte[] data, int offset, int length)
        {
            if (OnReceive != null)
            {
                if (!queue.ContainsKey(connectId))
                {
                    DataContainer container = new DataContainer { Length = 0, Data = new List<byte>() };
                    queue.Add(connectId, container);
                }
                byte[] r = new byte[length];
                Buffer.BlockCopy(data, offset, r, 0, length);
                queue[connectId].Data.AddRange(r);
                bool isContinueGetData = false;
                while(queue[connectId].Data.Count>8 && !isContinueGetData)
                {
                    byte[] datas = Read(connectId,out isContinueGetData);
                    if (datas != null && datas.Length > 0)
                    {
                        OnReceive(connectId, datas);
                    }
                }
            }
        }
        private void TcpServer_OnClose(int connectId)
        {
            if (queue.ContainsKey(connectId))
            {
                queue.Remove(connectId);
            }
            if (OnClose != null)
                OnClose(connectId);
        }
        private void TcpServer_OnAccept(int connectId)
        {
            if (OnAccept != null)
                OnAccept(connectId);
        }
        protected virtual byte[] AddHead(byte[] data)
        {
            uint len = (uint)data.Length;
            return System.BitConverter.GetBytes(headerFlag).Concat(System.BitConverter.GetBytes(len)).Concat(data).ToArray();
        }
        protected virtual byte[] Read(int connectId,out bool isContinueGetData)
        {
            if (!queue.ContainsKey(connectId))
            {
                isContinueGetData = true;
                return null;
            }
            DataContainer dc = queue[connectId];

            if (dc.Data.Count <= 8)
            {
                isContinueGetData = true;
                return null;
            }


            if (dc.Length == 0)//first time, parse len
            {
                uint checkHeaderFlag = BitConverter.ToUInt32(dc.Data.Take(4).ToArray(), 0);
                if (checkHeaderFlag != headerFlag)
                {
                    //if the format is not right , then remove all data and again
                    dc.Data.Clear();
                    dc.Length = 0;
                    queue[connectId] = dc;
                    isContinueGetData = true;
                    return null;
                }
                
                dc.Length = BitConverter.ToUInt32(dc.Data.Skip(4).Take(4).ToArray(), 0);
                queue[connectId] = dc;//set back to dictionary
            }


            if (dc.Length > dc.Data.Count - 8)
            {
                isContinueGetData = true;
                return null;
            }
            byte[] f = dc.Data.Skip(8).Take((int)dc.Length).ToArray();
            dc.Data.RemoveRange(0, 8 + (int)dc.Length);
            dc.Length = 0;
            isContinueGetData = false;
            return f;
        }
        #endregion

    }



}
