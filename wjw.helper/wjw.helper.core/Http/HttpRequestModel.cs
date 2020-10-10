using System;
using System.Collections.Generic;
using System.Text;

namespace wjw.helper.Http
{
    public class Request
    {
        public RequestHeader Header { get; set; }
        public RequestServer Server { get; set; }
        public string SerialNo { get; set; }
        public string TerminalID { get; set; }
        public string BranchCode { get; set; }
        public string TransactionType { get; set; }
        public string URL { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"SerialNo:{SerialNo}").AppendLine();
            sb.Append($"TerminalID:{TerminalID}").AppendLine();
            sb.Append($"BranchCode:{BranchCode}").AppendLine();
            sb.Append($"TransactionType:{TransactionType}").AppendLine();
            sb.Append($"URL:{URL}").AppendLine();
            sb.Append($"Content:{Content}");
            return sb.ToString();
        }
    }

    public class RequestHeader
    {
        public string Method { get; set; }
        public string ContentType { get; set; }
        public string RequestTimeout { get; set; }
        public string AllowAutoRedirect { get; set; }
        public string Headers { get; set; }
        public string Accept { get; set; }
        public string KeepAlive { get; set; }
        public string UserAgent { get; set; }

    }

    public class RequestServer
    {
        public string Protocol { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Encoding { get; set; }
        public string Certificate { get; set; }
        public string CertPw { get; set; }

    }
}
