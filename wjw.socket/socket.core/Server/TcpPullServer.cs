using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using wjw.socket.Common;
using System.Collections.Concurrent;

namespace wjw.socket.Server
{

    public class TcpPullServer
    {

        private TcpServer tcpServer;

        public event Action<int> OnAccept;

        public event Action<int, int> OnReceive;

        public event Action<int, int> OnSend;

        public event Action<int> OnClose;

        private Dictionary<int, List<byte>> queue;

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


        public TcpPullServer(int numConnections, int receiveBufferSize, int overtime)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                queue = new Dictionary<int, List<byte>>();
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
            {
                OnSend(connectId, length);
            }
        }
              

        private void TcpServer_eventactionReceive(int connectId, byte[] data, int offset, int length)
        {
            if (OnReceive != null)
            {
                if (!queue.ContainsKey(connectId))
                {
                    queue.Add(connectId, new List<byte>());
                }
                byte[] r = new byte[length];
                Buffer.BlockCopy(data, offset, r, 0, length);
                queue[connectId].AddRange(r);
                OnReceive(connectId, queue[connectId].Count);
            }
        }


        public int GetLength(int connectId)
        {
            if (!queue.ContainsKey(connectId))
            {
                return 0;
            }
            return queue[connectId].Count;
        }


        public byte[] Fetch(int connectId, int length)
        {
            if (!queue.ContainsKey(connectId))
            {
                return new byte[] { };
            }
            if (length > queue[connectId].Count)
            {
                length = queue[connectId].Count;
            }
            byte[] f = queue[connectId].Take(length).ToArray();
            queue[connectId].RemoveRange(0, length);
            return f;
        }

        public void Close(int connectId)
        {
            tcpServer.Close(connectId);
        }

        private void TcpServer_eventClose(int connectId)
        {
            if (queue.ContainsKey(connectId))
            {
                queue.Remove(connectId);
            }
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
