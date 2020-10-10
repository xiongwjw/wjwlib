using wjw.socket.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using wjw.socket.Client;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace wjw.socket.Busniness
{
    public class MessageClient : TcpPackClient
    {
        #region field
        private Thread _heartbeatThread = null;
        private Thread _recievedThread = null;
        private List<RegisterClientHandler> _handlerList = new List<RegisterClientHandler>();
        private ConcurrentQueue<string> _receivedQueue = new ConcurrentQueue<string>();
        private AutoResetEvent _synSignal = new AutoResetEvent(false);
        private List<string> _waittingMessage = new List<string>();
        private object responseMessage = null;
        #endregion

        #region event
        public event Action<string> OnReceivedMessage;
        public event Action<string> OnSendMessage;
        public event Action<object> OnReceivedObject;
        public new event Action<string> OnError;
        public new event Action OnClose;
        public new event Action<bool> OnConnect;
        #endregion

        #region public
        public MessageClient() : base(1024, 0xff)
        {
            base.OnReceive += MessageClient_OnReceive;
            base.OnError += MessageClient_OnError;
            base.OnConnect += MessageClient_OnConnect;
            base.OnClose += MessageClient_OnClose;
            base.OnSend += MessageClient_OnSend;

            _heartbeatThread = new Thread(HeartBeatThread);
            _heartbeatThread.IsBackground = true;
            _heartbeatThread.Start();

            _recievedThread = new Thread(HandleMessageThread);
            _recievedThread.IsBackground = true;
            _recievedThread.Start();

        }
        public void Register(Type messageType, Action<object> handler)
        {
            var _handler = _handlerList.Find(q => q.MessageType == messageType);
            if (_handler == null)
            {
                List<Action<object>> newHandle = new List<Action<object>>();
                newHandle.Add(handler);
                _handlerList.Add(new RegisterClientHandler
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
        public bool SendMessage<T>(object message) where T : BaseMessage
        {
            if (message == null || !(message is BaseMessage))
                return false;
            Type messageType = message.GetType();
            object SerialNo = messageType.GetProperty("SerialNo").GetValue(message);
            if (SerialNo == null || SerialNo.ToString() == string.Empty)
                messageType.GetProperty("SerialNo").SetValue(message, Guid.NewGuid().ToString("N"));
            messageType.GetProperty("MessageType").SetValue(message, typeof(T).Name);
            string strMessage = JsonExtension.ToJSON(message);
            byte[] data = Encoding.UTF8.GetBytes(strMessage);
            Send(data, 0, data.Length);
            OnSendMessage?.Invoke(strMessage);
            return true;
        }
        public bool SendMessage(object message)
        {
            if (message == null || !(message is BaseMessage))
                return false;
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
            Send(data, 0, data.Length);
            OnSendMessage?.Invoke(strMessage);
            return true;
        }
        public bool SendAndReceiveMessage(object message, out object response)
        {
            if (SendMessage(message))
            {
                if (Wait(message))
                {
                    response = responseMessage;
                    return true;
                }
                else
                {
                    response = null;
                    return false;
                }
            }
            else
            {
                response = null;
                return false;
            }
        }
        #endregion

        #region private
        private void MessageClient_OnClose()
        {
            OnClose?.Invoke();
        }
        private void MessageClient_OnError(string message)
        {
            OnError?.Invoke(message);
        }
        private void MessageClient_OnConnect(bool obj)
        {
            OnConnect?.Invoke(obj);
        }
        private void MessageClient_OnReceive(byte[] obj)
        {
            string strMessage = Encoding.UTF8.GetString(obj);
            OnReceivedMessage?.Invoke(strMessage);
            _receivedQueue.Enqueue(strMessage);
        }
        private void MessageClient_OnSend(int obj)
        {
            //do nothing now
        }
        private bool Wait(object message)
        {
            BaseMessage baseMessage = message as BaseMessage;
            _waittingMessage.Add(baseMessage.SerialNo);
            _synSignal.Reset();
            if (_synSignal.WaitOne(10000))//wait 10 sec will timeout
                return true;
            else
                return false;
        }
        private void CheckWattingQueue(object message)
        {
            BaseMessage baseMessage = message as BaseMessage;
            if(_waittingMessage.Contains(baseMessage.SerialNo))
            {
                _waittingMessage.Remove(baseMessage.SerialNo);
                responseMessage = message;
                _synSignal.Set();
            }
        }
        private void HandleMessageThread()
        {
            while (true)
            {
                string strMessage = string.Empty;
                while (_receivedQueue.TryDequeue(out strMessage))
                {
                    var handler = _handlerList.Find(q => q.MessageType.Name == Utility.GetMessageType(strMessage));
                    if (handler != null && handler.Handlers.Count>0)
                    {
                        object message = JsonExtension.FromJSON(handler.MessageType, strMessage);
                        //try waitting queue
                        CheckWattingQueue(message);
                        foreach (Action<object> action in handler.Handlers)
                            if(action!=null)
                                action(message);
                        OnReceivedObject?.Invoke(message);
                    }
                    else
                    {
                        // if no handler, directly convert to object, but what this object will be??
                        object message = JsonExtension.FromJSON(strMessage);
                        OnReceivedObject?.Invoke(message);
                    }
                }
                Thread.Sleep(1000);
            }
        }
        private void HeartBeatThread()
        {
            while (true)
            {
                if (this.Connected)
                {
                    byte[] data = Encoding.UTF8.GetBytes("H");
                    Send(data, 0, data.Length);
                }
                Thread.Sleep(10000);
            }
        }
        #endregion

    }
}
