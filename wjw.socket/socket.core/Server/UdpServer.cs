
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using wjw.socket.Common;

namespace wjw.socket.Server
{
    public class UdpServer
    {
        private Socket listenSocket;

        private int m_receiveBufferSize;

        private Mutex mutex = new Mutex();

        private SocketAsyncEventArgsPool m_sendPool;

        private int sendthread = 10;

        private ConcurrentQueue<SendingQueue>[] sendQueues;

        public event Action<EndPoint, byte[], int, int> OnReceive;

        public event Action<EndPoint, int> OnSend;

        public UdpServer(int receiveBufferSize)
        {
            m_receiveBufferSize = receiveBufferSize;
            m_sendPool = new SocketAsyncEventArgsPool(10);
            Init();
        }

        private void Init()
        {
            sendQueues = new ConcurrentQueue<SendingQueue>[sendthread];
            for (int i = 0; i < sendthread; i++)
            {
                sendQueues[i] = new ConcurrentQueue<SendingQueue>();
            }
        }

        public void Start(int port, bool reuseAddress = false)
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
            listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            listenSocket.ExclusiveAddressUse = !reuseAddress;
            listenSocket.SetSocketOption(
                SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, reuseAddress);
            listenSocket.Bind(localEndPoint);
            byte[] receivebuffer = new byte[m_receiveBufferSize];
            SocketAsyncEventArgs receiveSocketArgs = new SocketAsyncEventArgs();
            receiveSocketArgs.RemoteEndPoint = localEndPoint;
            receiveSocketArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
            receiveSocketArgs.SetBuffer(receivebuffer, 0, receivebuffer.Length);
            StartReceive(receiveSocketArgs);
            for (int i = 0; i < sendthread; i++)
            {
                Thread thread = new Thread(StartSend);
                thread.IsBackground = true;
                thread.Priority = ThreadPriority.AboveNormal;
                thread.Start(i);
            }
        }

        private void StartReceive(SocketAsyncEventArgs receiveSocketArgs)
        {
            if (!listenSocket.ReceiveFromAsync(receiveSocketArgs))
            {
                ProcessReceive(receiveSocketArgs);
            }
        }

        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                if (OnReceive != null)
                {
                    OnReceive(e.RemoteEndPoint, e.Buffer, e.Offset, e.BytesTransferred);
                }
            }
            StartReceive(e);
        }

        private void StartSend(object thread)
        {
            while (true)
            {
                SendingQueue sending;
                if (sendQueues[(int)thread].TryDequeue(out sending))
                {
                    Send(sending);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

        public void Send(EndPoint remoteEndPoint, byte[] data, int offset, int length)
        {
            sendQueues[((IPEndPoint)remoteEndPoint).Port % sendthread].Enqueue(new SendingQueue() { remoteEndPoint = remoteEndPoint, data = data, offset = offset, length = length });
        }

        private void Send(SendingQueue sendQuere)
        {
            mutex.WaitOne();
            if (m_sendPool.Count == 0)
            {
                SocketAsyncEventArgs saea_send = new SocketAsyncEventArgs();
                saea_send.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                m_sendPool.Push(saea_send);
            }
            SocketAsyncEventArgs socketArgs = m_sendPool.Pop();
            mutex.ReleaseMutex();
            socketArgs.RemoteEndPoint = sendQuere.remoteEndPoint;
            socketArgs.SetBuffer(sendQuere.data, sendQuere.offset, sendQuere.length);
            if (!listenSocket.SendToAsync(socketArgs))
            {
                ProcessSend(socketArgs);
            }
        }

        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                m_sendPool.Push(e);
                if (OnSend != null)
                {
                    OnSend(e.RemoteEndPoint, e.BytesTransferred);
                }
            }
        }

        private void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.ReceiveFrom:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.SendTo:
                    ProcessSend(e);
                    break;
                default:
                    break;
            }
        }


    }
}
