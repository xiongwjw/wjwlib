using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace wjw.socket.Client
{
    public class TcpPullClient
    {
        private TcpClients tcpClients;
        private List<byte> queue;
        private Mutex mutex = new Mutex();

        public event Action<bool> OnConnect;
        public event Action<int> OnReceive;
        public event Action<int> OnSend;
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
        public TcpPullClient(int receiveBufferSize)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                queue = new List<byte>();
                tcpClients = new TcpClients(receiveBufferSize);
                tcpClients.OnConnect += TcpServer_eventactionConnect;
                tcpClients.OnReceive += TcpServer_eventactionReceive;
                tcpClients.OnSend += TcpClients_OnSend;
                tcpClients.OnClose += TcpServer_eventClose;
            }));
            thread.IsBackground = true;
            thread.Start();
        }
        public void Connect(string ip, int port)
        {
            while (tcpClients == null)
            {
                Thread.Sleep(10);
            }
            tcpClients.Connect(ip, port);
        }
        public void Send(byte[] data, int offset, int length)
        {
            tcpClients.Send(data, offset, length);
        }
        public int GetLength()
        {
            return queue.Count;
        }
        public byte[] Fetch(int length)
        {
            mutex.WaitOne();
            if (length > queue.Count)
            {
                length = queue.Count;
            }
            byte[] f = queue.Take(length).ToArray();
            queue.RemoveRange(0, length);
            mutex.ReleaseMutex();
            return f;
        }
        public void Close()
        {
            tcpClients.Close();
        }

        private void TcpClients_OnSend(int length)
        {
            if (OnSend != null)
                OnSend(length);
        }
        private void TcpServer_eventactionConnect(bool success)
        {
            if (OnConnect != null)
                OnConnect(success);
        }
        private void TcpServer_eventactionReceive(byte[] data, int offset, int length)
        {
            if (OnReceive != null)
            {
                byte[] r = new byte[length];
                Buffer.BlockCopy(data, offset, r, 0, length);
                queue.AddRange(r);
                OnReceive(queue.Count);
            }
        }
        private void TcpServer_eventClose()
        {
            queue.Clear();
            if (OnClose != null)
                OnClose();
        }
    }
}
