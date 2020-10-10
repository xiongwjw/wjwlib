using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

namespace wjw.socket.Client
{

    public class TcpPushClient
    {
        private TcpClients tcpClients;

        public event Action<bool> OnConnect;
        public event Action<byte[], int, int> OnReceive;
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
        public TcpPushClient(int receiveBufferSize)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
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
        public void Close()
        {
            tcpClients.Close();
        }
        public void Send(byte[] data, int offset, int length)
        {
            tcpClients.Send(data, offset, length);
        }

        private void TcpClients_OnSend(int length)
        {
            if (OnSend != null)
                OnSend(length);
        }
        private void TcpServer_eventactionReceive(byte[] data, int offset, int length)
        {
            if (OnReceive != null)
                OnReceive(data,offset,length);
        }
        private void TcpServer_eventactionConnect(bool success)
        {
            if (OnConnect != null)
                OnConnect(success);
        }
        private void TcpServer_eventClose()
        {
            if (OnClose != null)
                OnClose();
        }



    }
}
