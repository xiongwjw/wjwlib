using System;
using System.Collections.Generic;
using System.Text;
using wjw.socket.Server;
using wjw.socket.Common;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;
using System.Threading;
using System.Reflection;

namespace wjw.socket.Busniness
{
    public class MessageServer : TcpPackServer
    {
        #region field
        private Thread _recievedThread = null;
        private ConcurrentQueue<ReceivedMessage> _receivedQueue = new ConcurrentQueue<ReceivedMessage>();
        private List<RegisterServerHandler> _handlerList = new List<RegisterServerHandler>();
        #endregion

        #region event
        public event Action<string,int,int, string> OnReceivedMessage;
        public event Action<string,int,int,string> OnSendMessage;
        public event Action<string,int,int,object> OnReceivedObject;
        public new event Action<string,int,int> OnClose;//ip,port,id
        public new event Action<string,int,int> OnAccept;
        public new event Action<string> OnError;
        #endregion event

        #region public
        public MessageServer():base(2000,1024,15,0xff)
        {
            base.OnReceive += MessageServer_OnReceive;
            base.OnError += MessageServer_OnError;
            base.OnAccept += MessageServer_OnAccept;
            base.OnClose += MessageServer_OnClose;
            base.OnSend += MessageServer_OnSend;
            _recievedThread = new Thread(HandleMessageThread);
            _recievedThread.IsBackground = true;
            _recievedThread.Start();
        }
        public void Register(Type messageType,  Action<int,object>  handler)
        {
            var _handler = _handlerList.Find(q => q.MessageType == messageType);
            if (_handler == null)
            {
                List<Action<int,object>> newHandle = new List<Action<int,object>>();
                newHandle.Add(handler);
                _handlerList.Add(new RegisterServerHandler
                {
                    MessageType = messageType,
                    Handlers = newHandle
                });
            }
            else
            {
                _handler.Handlers.Add(handler);
            }
        }
        public bool SendMessage(string ip, object message)
        {
            if (message == null || !(message is BaseMessage))
                return false;
            byte[] data = ConstructData(message);
            Send(ip, data, 0, data.Length);
            OnSendMessage?.Invoke(ip,0,0,$"{JsonExtension.ToJSON(message)}");
            return true;
        }
        public bool SendMessage(int connectID, object message)
        {
            if (message == null || !(message is BaseMessage))
                return false;
            byte[] data = ConstructData(message);
            Send(connectID, data, 0, data.Length);
            OnSendMessage?.Invoke(GetClientIpById(connectID),GetClientPortIpById(connectID),connectID, $"{JsonExtension.ToJSON(message)}");
            return true;
        }
        public void SendMessageToAll(object message)
        {
            byte[] data = ConstructData(message);
            SendToAllClient(data, 0, data.Length);
            OnSendMessage?.Invoke("0",0,0,$"{JsonExtension.ToJSON(message)}");
        }
        public void SendMessage<T>(int connectID, object message) where T : BaseMessage
        {
            byte[] data = ConstructData<T>(message);
            Send(connectID, data, 0, data.Length);
            OnSendMessage?.Invoke(GetClientIpById(connectID),GetClientPortIpById(connectID),connectID, $"{JsonExtension.ToJSON(message)}");
        }
        public bool SendMessage<T>(string ip, object message) where T : BaseMessage
        {
            if (message == null || !(message is BaseMessage))
                return false;
            byte[] data = ConstructData<T>(message);
            Send(ip, data, 0, data.Length);
            OnSendMessage?.Invoke(ip,0,0,$"{JsonExtension.ToJSON(message)}");
            return true;
        }
        public void SendMessageToAll<T>(object message) where T : BaseMessage
        {
            byte[] data = ConstructData<T>(message);
            SendToAllClient(data, 0, data.Length);
            OnSendMessage?.Invoke("0",0,0, $"{JsonExtension.ToJSON(message)}");
        }
        #endregion

        #region private
        private byte[] ConstructData(object message)
        {
            Type messageType = message.GetType();

            PropertyInfo serialNoProperty = messageType.GetProperty("SerialNo");
            if (serialNoProperty != null)
            {
                object SerialNo = serialNoProperty.GetValue(message);
                if (SerialNo == null || SerialNo.ToString() == string.Empty)
                    serialNoProperty.SetValue(message, Guid.NewGuid().ToString("N"));
            }

            PropertyInfo messageTypeProperty = messageType.GetProperty("MessageType");
            if (messageTypeProperty != null)
            {
                messageTypeProperty.SetValue(message, messageType.Name);
            }

            string strMessage = JsonExtension.ToJSON(message);
            byte[] data = Encoding.UTF8.GetBytes(strMessage);
            return data;
        }
        private byte[] ConstructData<T>(object message) where T : BaseMessage
        {
            Type messageType = message.GetType();
            object SerialNo = messageType.GetProperty("SerialNo").GetValue(message);
            if (SerialNo == null || SerialNo.ToString() == string.Empty)
                messageType.GetProperty("SerialNo").SetValue(message, Guid.NewGuid().ToString("N"));
            messageType.GetProperty("MessageType").SetValue(message, typeof(T).Name);
            string strMessage = JsonExtension.ToJSON(message);
            byte[] data = Encoding.UTF8.GetBytes(strMessage);
            return data;
        }
        private void HandleMessageThread()
        {
            while (true)
            {
                ReceivedMessage receivedMessage = new ReceivedMessage();
                while (_receivedQueue.TryDequeue(out receivedMessage))
                {
                    var handler = _handlerList.Find(q => q.MessageType.Name == Utility.GetMessageType(receivedMessage.message));
                    if (handler != null & handler.Handlers.Count> 0)
                    {
                        object message = JsonExtension.FromJSON(handler.MessageType, receivedMessage.message);
                        foreach (Action<int, object> handle in handler.Handlers)
                        {
                            //Task.Run(() => handle(arg1, message));
                            if (handle != null)
                                handle(receivedMessage.connectID, message);
                        }
                        OnReceivedObject?.Invoke(GetClientIpById(receivedMessage.connectID),GetClientPortIpById(receivedMessage.connectID),receivedMessage.connectID,message);
                    }
                    else
                    {
                        // if no handler, directly convert to object, but what this object will be??
                        object message = JsonExtension.FromJSON(receivedMessage.message);
                        OnReceivedObject?.Invoke(GetClientIpById(receivedMessage.connectID), GetClientPortIpById(receivedMessage.connectID), receivedMessage.connectID, message);
                    }
                }
                Thread.Sleep(1000);
            }
        }
        private void MessageServer_OnReceive(int arg1, byte[] arg2)
        {
            string strMessage = Encoding.UTF8.GetString(arg2);
            if (strMessage.Equals("H"))//this is heartbeat package, leave it.
                return;
            OnReceivedMessage?.Invoke(GetClientIpById(arg1), GetClientPortIpById(arg1), arg1, strMessage);
            _receivedQueue.Enqueue(new ReceivedMessage { connectID = arg1, message = strMessage });
        }
        private void MessageServer_OnClose(int connectID)
        {
            OnClose?.Invoke(GetClientIpById(connectID),GetClientPortIpById(connectID),connectID);
        }
        private void MessageServer_OnAccept(int connectID)
        {
            OnAccept?.Invoke(GetClientIpById(connectID), GetClientPortIpById(connectID), connectID);
        }
        private void MessageServer_OnError(string message)
        {
            OnError?.Invoke(message);
        }
        private void MessageServer_OnSend(int arg1, int arg2)
        {
            // throw new NotImplementedException();
            //do nothing here now
        }
        #endregion

        private class ReceivedMessage
        {
            public int connectID { get; set; } = 0;
            public string message { get; set; } = string.Empty;
        }

    }
}
