using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using wjw.socket.Common;

namespace wjw.socket.Client
{
    internal class TcpClients
    {
        #region field
        private Socket socket;
        private SocketAsyncEventArgsPool m_sendPool;
        private int m_receiveBufferSize;
        private byte[] buffer_receive;
        private SocketAsyncEventArgs receiveSocketAsyncEventArgs;
        private int m_minSendSocketAsyncEventArgs = 10;
        private ConcurrentQueue<SendingQueue> sendQueue;
        private Mutex mutex = new Mutex();
        #endregion

        #region event
        internal event Action<bool> OnConnect;
        internal event Action<byte[], int, int> OnReceive;
        internal event Action<int> OnSend;
        internal event Action OnClose;
        internal event Action<string> OnError;
        public bool Connected
        {
            get
            {
                if (socket == null)
                {
                    return false;
                }
                return socket.Connected;
            }
        }
        #endregion

        #region internal
        internal TcpClients(int receiveBufferSize)
        {
            m_receiveBufferSize = receiveBufferSize;
            m_sendPool = new SocketAsyncEventArgsPool(m_minSendSocketAsyncEventArgs);
            Init();
        }
        internal void Close()
        {
            CloseClientSocket(receiveSocketAsyncEventArgs);
        }
        internal void Connect(string ip, int port)
        {
            IPAddress ipaddr;
            if (!IPAddress.TryParse(ip, out ipaddr))
            {
                IPAddress[] iplist = Dns.GetHostAddresses(ip);
                if (iplist != null && iplist.Length > 0)
                {
                    ipaddr = iplist[0];
                }
            }
            IPEndPoint localEndPoint = new IPEndPoint(ipaddr, port);
            socket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.NoDelay = true;
            SocketAsyncEventArgs connSocketAsyncEventArgs = new SocketAsyncEventArgs();
            connSocketAsyncEventArgs.RemoteEndPoint = localEndPoint;
            connSocketAsyncEventArgs.Completed += IO_Completed;
            if (!socket.ConnectAsync(connSocketAsyncEventArgs))
            {
                ProcessConnect(connSocketAsyncEventArgs);
            }
        }
        internal void Send(byte[] data, int offset, int length)
        {
            sendQueue.Enqueue(new SendingQueue() { data = data, offset = offset, length = length });
        }
        internal void Send(SendingQueue sendQuere)
        {
            if (!socket.Connected)
            {
                return;
            }
            mutex.WaitOne();
            if (m_sendPool.Count == 0)
            {
                SocketAsyncEventArgs saea_send = new SocketAsyncEventArgs();
                saea_send.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                m_sendPool.Push(saea_send);
            }
            SocketAsyncEventArgs sendEventArgs = m_sendPool.Pop();
            mutex.ReleaseMutex();
            sendEventArgs.SetBuffer(sendQuere.data, sendQuere.offset, sendQuere.length);
            if (!socket.SendAsync(sendEventArgs))
            {
                ProcessSend(sendEventArgs);
            }
        }
        #endregion

        #region private
        private void ProcessConnect(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                receiveSocketAsyncEventArgs = new SocketAsyncEventArgs();
                receiveSocketAsyncEventArgs.SetBuffer(buffer_receive, 0, buffer_receive.Length);
                receiveSocketAsyncEventArgs.Completed += IO_Completed;
                if (!socket.ReceiveAsync(receiveSocketAsyncEventArgs))
                {
                    ProcessReceive(receiveSocketAsyncEventArgs);
                }
                if (OnConnect != null)
                {
                    OnConnect(true);
                }
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    StartSend();
                }));
                thread.IsBackground = true;
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            else
            {
                if (OnConnect != null)
                {
                    OnConnect(false);
                }
            }
        }
        private void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                case SocketAsyncOperation.Connect:
                    ProcessConnect(e);
                    break;
                default:
                    OnError?.Invoke("The last operation completed on the socket is not receive or send or connect");
                    break;
            }
        }
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                byte[] data = new byte[e.BytesTransferred];
                if (OnReceive != null)
                {
                    OnReceive(e.Buffer, e.Offset, e.BytesTransferred);
                }
                if (socket.Connected == true)
                {
                    while (!socket.ReceiveAsync(e))
                    {
                        OnReceive(e.Buffer, e.Offset, e.BytesTransferred);
                    }

                }
            }
            else
            {
                CloseClientSocket(e);
            }
        }
        private void Init()
        {
            buffer_receive = new byte[m_receiveBufferSize];
            sendQueue = new ConcurrentQueue<SendingQueue>();
        }
        private void StartSend()
        {
            while (true)
            {
                SendingQueue sending;
                if (sendQueue.TryDequeue(out sending))
                {
                    Send(sending);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                m_sendPool.Push(e);
                if (OnSend != null)
                {
                    OnSend(e.BytesTransferred);
                }
            }
        }
        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            if (!socket.Connected)
            {
                return;
            }
            if (e.LastOperation == SocketAsyncOperation.Receive)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                }
                catch (Exception) { }
                socket.Close();
                if (OnClose != null)
                {
                    OnClose();
                }
            }
        }
        #endregion
    }
}
