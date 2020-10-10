using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Concurrent;
using wjw.socket.Common;

namespace wjw.socket.Server
{

    public class TcpPushServer
    {

        private TcpServer tcpServer;

        public event Action<int> OnAccept;

        public event Action<int, byte[]> OnReceive;

        public event Action<int, int> OnSend;

        public event Action<int> OnClose;

        public ConcurrentDictionary<int, string> ClientList
        {
            get
            {
                if(tcpServer!=null)
                {
                    return tcpServer.clientList;
                }
                else
                {
                    return new ConcurrentDictionary<int, string>();
                }                
            }
        }

        public TcpPushServer(int numConnections, int receiveBufferSize, int overtime)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                tcpServer = new TcpServer(numConnections, receiveBufferSize, overtime);
                tcpServer.OnAccept += TcpServer_eventactionAccept;
                tcpServer.OnReceive += TcpServer_eventactionReceive;
                tcpServer.OnSend += TcpServer_OnSend;
                tcpServer.OnClose += TcpServer_eventClose;
            }));
            thread.IsBackground = true;
            thread.Start();
        }


        public void Start(int port)
        {
            while (tcpServer == null)
            {
                Thread.Sleep(10);
            }
            tcpServer.Start(port);
        }


        private void TcpServer_eventactionAccept(int connectId)
        {
            if (OnAccept != null)
                OnAccept(connectId);
        }


        public void Send(int connectId, byte[] data, int offset, int length)
        {
            tcpServer.Send(connectId, data, offset, length);
        }

        private void TcpServer_OnSend(int connectId, int length)
        {
            if (OnSend != null)
                OnSend(connectId, length);
        }


        private void TcpServer_eventactionReceive(int connectId, byte[] data,int offset,int length)
        {
            if (OnReceive != null)
            {
                byte[] da = new byte[length];
                Buffer.BlockCopy(data, offset, da, 0, length);
                OnReceive(connectId, da);
            }
        }

        public void Close(int connectId)
        {
            tcpServer.Close(connectId);
        }


        private void TcpServer_eventClose(int connectId)
        {
            if (OnClose != null)
                OnClose(connectId);
        }

        public bool SetAttached(int connectId, object data)
        {
            return tcpServer.SetAttached(connectId, data);
        }

        public T GetAttached<T>(int connectId)
        {
            return tcpServer.GetAttached<T>(connectId);
        }
    }
}
