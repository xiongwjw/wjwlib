using System;
using System.Collections.Generic;
using System.Text;

namespace wjw.socket.Common
{
    public class BaseMessage
    {
        public string SerialNo { get; set; }
        public string MessageType {get;set;}
    }

    public class Request : BaseMessage
    {
        public RequestHeader Header { get; set; }
    }
    public class Response : BaseMessage
    {
        public ResponseHeader Header { get; set; }
    }

    public class Message : BaseMessage
    {

    }
    public class RequestHeader
    {
       
    }

    public class ResponseHeader
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
    }

    public struct ResponseResult
    {
        public const string SUCCESS = "0";
        public const string FAILED = "1";
    }

    internal class RegisterClientHandler
    {
        public Type MessageType { get; set; }
        public List<Action<object>> Handlers { get; set; }
    }

    internal class RegisterServerHandler
    {
        public Type MessageType { get; set; }
        public List<Action<int,object>> Handlers { get; set; }
    }


}
