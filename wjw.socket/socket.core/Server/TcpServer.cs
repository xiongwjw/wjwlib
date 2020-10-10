using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;
using System.Collections.Concurrent;
using wjw.socket.Common;

namespace wjw.socket.Server
{

    internal class TcpServer
    {
        #region field
        private int connectId;
        private int m_numConnections;
        private int m_receiveBufferSize;
        private BufferManager m_bufferManager;
        private Socket listenSocket;
        private SocketAsyncEventArgsPool m_receivePool;
        private SocketAsyncEventArgsPool m_sendPool;
        private int overtime;
        private int overtimecheck = 1;
        private Semaphore m_maxNumberAcceptedClients;
        private int sendthread = 10;
        private ConcurrentQueue<SendingQueue>[] sendQueues;
        private Mutex mutex = new Mutex();
        private ConcurrentDictionary<int, ConnectClient> connectClient;
        #endregion

        #region event
        internal ConcurrentDictionary<int, string> clientList;
        internal event Action<int> OnAccept;
        internal event Action<int, byte[], int, int> OnReceive;
        internal event Action<int, int> OnSend;
        internal event Action<int> OnClose;
        internal event Action<string> OnError;
        #endregion

        #region internal
        internal TcpServer(int numConnections, int receiveBufferSize, int overTime)
        {
            overtime = overTime;
            m_numConnections = numConnections;
            m_receiveBufferSize = receiveBufferSize;
            m_bufferManager = new BufferManager(receiveBufferSize * m_numConnections, receiveBufferSize);
            m_receivePool = new SocketAsyncEventArgsPool(m_numConnections);
            m_sendPool = new SocketAsyncEventArgsPool(m_numConnections);
            m_maxNumberAcceptedClients = new Semaphore(m_numConnections, m_numConnections);
            Init();
        }
        internal bool SetAttached(int connectId, object data)
        {
            ConnectClient client;
            if (!connectClient.TryGetValue(connectId, out client))
            {
                return false;
            }
            client.attached = data;
            return true;
        }
        internal T GetAttached<T>(int connectId)
        {
            ConnectClient client;
            if (!connectClient.TryGetValue(connectId, out client))
            {
                return default(T);
            }
            else
            {
                return (T)client.attached;
            }
        }
        internal void Start(int port)
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
            listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.NoDelay = true;
            listenSocket.Bind(localEndPoint);
            listenSocket.Listen(1000);
            StartAccept(null);
            for (int i = 0; i < sendthread; i++)
            {
                Thread thread = new Thread(StartSend);
                thread.IsBackground = true;
                thread.Priority = ThreadPriority.AboveNormal;
                thread.Start(i);
            }
            if (overtime > 0)
            {
                Thread heartbeat = new Thread(new ThreadStart(() =>
                {
                    Heartbeat();
                }));
                heartbeat.IsBackground = true;
                heartbeat.Priority = ThreadPriority.Lowest;
                heartbeat.Start();
            }
        }
        internal void Send(int connectId, byte[] data, int offset, int length)
        {
            sendQueues[connectId % sendthread].Enqueue(new SendingQueue() { connectId = connectId, data = data, offset = offset, length = length });
        }
        internal void Send(string ip, byte[] data, int offset, int length)
        {
            var clients = clientList.Where(q => GetIp(q.Value) != string.Empty && GetIp(q.Value) == ip);
            if (!default(KeyValuePair<int, string>).Equals(clients))
            {
                foreach (KeyValuePair<int, string> client in clients)
                {
                    if (client.Value != null)
                    {
                        int connectId = client.Key;
                        sendQueues[connectId % sendthread].Enqueue(new SendingQueue() { connectId = connectId, data = data, offset = offset, length = length });
                    }
                }
            }
        }
        internal bool IsClientConnect(string ip)
        {
            if (clientList.Count == 0)
                return false;
            var clients = clientList.FirstOrDefault(q => GetIp(q.Value) != string.Empty && GetIp(q.Value) == ip);
            if (clients.Value != null)
            {
                return true;
            }
            return false;
        }
        internal void SendToAllClient(byte[] data, int offset, int length)
        {
            foreach (KeyValuePair<int, string> client in clientList)
            {
                sendQueues[client.Key % sendthread].Enqueue(new SendingQueue() { connectId = client.Key, data = data, offset = offset, length = length });

            }
        }
        internal void Close(int connectId)
        {
            ConnectClient client;
            if (!connectClient.TryGetValue(connectId, out client))
            {
                return;
            }
            CloseClientSocket(client.saea_receive);
        }
        internal string GetClientIpById(int connectID)
        {
            var client = clientList.FirstOrDefault(q => q.Key == connectID);
            if (client.Value != null)
            {
                return GetIp(client.Value);
            }
            else
                return string.Empty;
        }
        internal int GetClientPortById(int connectID)
        {
            var client = clientList.FirstOrDefault(q => q.Key == connectID);
            if (client.Value != null)
            {
                return GetPort(client.Value);
            }
            else
                return 0;
        }
        internal string GetEndpoint(int connectID)
        {
            var client = clientList.FirstOrDefault(q => q.Key == connectID);
            if (client.Value != null)
            {
                return client.Value;
            }
            return string.Empty;
        }
        internal void Close()
        {
            //close all client first. 
            foreach (var client in clientList)
            {
                Close(client.Key);
            }

            //shutdown socket
            try
            {
                listenSocket.Shutdown(SocketShutdown.Both);
                listenSocket.Close();
            }
            catch (Exception) { }
        }
        #endregion

        #region private
        private void Init()
        {
            connectClient = new ConcurrentDictionary<int, ConnectClient>();
            clientList = new ConcurrentDictionary<int, string>();
            sendQueues = new ConcurrentQueue<SendingQueue>[sendthread];
            for (int i = 0; i < sendthread; i++)
            {
                sendQueues[i] = new ConcurrentQueue<SendingQueue>();
            }
            m_bufferManager.InitBuffer();
            SocketAsyncEventArgs saea_receive;
            SocketAsyncEventArgs saea_send;
            for (int i = 0; i < m_numConnections; i++)
            {
                saea_receive = new SocketAsyncEventArgs();
                saea_receive.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                m_bufferManager.SetBuffer(saea_receive);
                m_receivePool.Push(saea_receive);
                saea_send = new SocketAsyncEventArgs();
                saea_send.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                m_sendPool.Push(saea_send);
            }
        }
        private void Heartbeat()
        {
            int count = overtime / overtimecheck;
            while (true)
            {
                foreach (var item in connectClient.Values)
                {
                    if (item.keep_alive >= count)
                    {
                        //Log.LogInfo($"keep alive value {item.keep_alive}");
                        item.keep_alive = 0;
                        //test first ,close heartbeat check
                        CloseClientSocket(item.saea_receive); 
                    }
                }
                foreach (var item in connectClient.Values)
                {
                    item.keep_alive++;
                }
                Thread.Sleep(overtimecheck * 1000);
            }
        }
        private void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
            }
            else
            {
                acceptEventArg.AcceptSocket = null;
            }
            m_maxNumberAcceptedClients.WaitOne();
            if (!listenSocket.AcceptAsync(acceptEventArg))
            {
                ProcessAccept(acceptEventArg);
            }
        }
        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            connectId++;
            ConnectClient connecttoken = new ConnectClient();
            connecttoken.socket = e.AcceptSocket;
            connecttoken.saea_receive = m_receivePool.Pop();
            connecttoken.saea_receive.UserToken = connectId;
            connecttoken.saea_receive.AcceptSocket = e.AcceptSocket;
            connectClient.TryAdd(connectId, connecttoken);
            clientList.TryAdd(connectId, e.AcceptSocket.RemoteEndPoint.ToString());
            if (!e.AcceptSocket.ReceiveAsync(connecttoken.saea_receive))
            {
                ProcessReceive(connecttoken.saea_receive);
            }
            if (OnAccept != null)
            {
                OnAccept(connectId);
            }
            StartAccept(e);
            
        }
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                int connectId = (int)e.UserToken;
                ConnectClient client;
                if (!connectClient.TryGetValue(connectId, out client))
                {
                    return;
                }
                if (overtime > 0)
                {
                    if (client != null)
                    {
                        client.keep_alive = 0;
                    }
                }
                if (OnReceive != null)
                {
                    if (client != null)
                    {
                        OnReceive(connectId, e.Buffer, e.Offset, e.BytesTransferred);
                    }
                }

                while(!e.AcceptSocket.ReceiveAsync(e))
                {
                    OnReceive(connectId, e.Buffer, e.Offset, e.BytesTransferred);
                }
            }
            else
            {
                CloseClientSocket(e);
            }
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
        private string GetIp(string endpoint)
        {
            string[] arr = endpoint.Split(':');
            if (arr.Length == 2)
                return arr[0];
            return string.Empty;
        }
        private int GetPort(string endpoint)
        {
            string[] arr = endpoint.Split(':');
            if (arr.Length == 2)
            {
                int result = 0;
                if (int.TryParse(arr[1], out result))
                    return result;
                else
                    return 0;
            }
            return 0;
        }
        private void Send(SendingQueue sendQuere)
        {
            ConnectClient client;
            if (!connectClient.TryGetValue(sendQuere.connectId, out client))
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
            sendEventArgs.UserToken = sendQuere.connectId;
            sendEventArgs.SetBuffer(sendQuere.data, sendQuere.offset, sendQuere.length);
            try
            {
                if (!client.socket.SendAsync(sendEventArgs))
                {
                    ProcessSend(sendEventArgs);
                }
            }
            catch (ObjectDisposedException ex)
            {
                
                OnError?.Invoke(ex.Message);
                if (OnClose != null)
                {
                    OnClose(sendQuere.connectId);
                }
            }
            sendQuere = null;
        }
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                m_sendPool.Push(e);
                if (OnSend != null)
                {
                    OnSend((int)e.UserToken, e.BytesTransferred);
                }
            }
            else
            {
                CloseClientSocket(e);
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
                case SocketAsyncOperation.Accept:
                    ProcessAccept(e);
                    break;
                default:
                    break;
            }
        }
        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            if (e.LastOperation == SocketAsyncOperation.Receive)
            {
                int connectId = (int)e.UserToken;
                ConnectClient client;
                string clientip;
                if (!connectClient.TryGetValue(connectId, out client))
                {
                    return;
                }
                try
                {
                    client.socket.Shutdown(SocketShutdown.Both);
                    client.socket.Close();
                    m_receivePool.Push(e);
                    m_maxNumberAcceptedClients.Release();

                    connectClient.TryRemove(connectId, out client);
                    clientList.TryRemove(connectId, out clientip);
                    client = null;
                }
                catch (Exception ex) {
                    OnError?.Invoke(ex.Message);
                }

                if (OnClose != null)
                {
                    OnClose(connectId);
                }

            }
        }
        #endregion

    }

}