using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Http
{
    public class HttpRequest
    {
        public static string Post(Request requestTc)
        {
            string strResult = string.Empty;
            HttpWebRequest request = null;
            try
            {
                string url = requestTc.Server.Protocol + "://" + requestTc.Server.Host + ":" + requestTc.Server.Port + requestTc.URL;
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = requestTc.Header.Method;
                request.ContentType = requestTc.Header.ContentType;
                request.Timeout = int.Parse(requestTc.Header.RequestTimeout);
                request.ReadWriteTimeout = int.Parse(requestTc.Header.RequestTimeout);

                if (!string.IsNullOrEmpty(requestTc.Header.AllowAutoRedirect))
                    request.AllowAutoRedirect = ParseBool(requestTc.Header.AllowAutoRedirect);

                if (!string.IsNullOrEmpty(requestTc.Header.Headers))
                {
                    Dictionary<string, string> dict = GetHeader(requestTc.Header.Headers);
                    foreach (var item in dict)
                        request.Headers.Add(item.Key, item.Value);
                }

                if (!string.IsNullOrEmpty(requestTc.Header.Accept))
                    request.Accept = requestTc.Header.Accept;

                if (!string.IsNullOrEmpty(requestTc.Header.KeepAlive))
                    request.KeepAlive = ParseBool(requestTc.Header.KeepAlive);

                if (!string.IsNullOrEmpty(requestTc.Header.UserAgent))
                    request.UserAgent = requestTc.Header.UserAgent;

                // https
                if (IsHttps(requestTc.Server.Protocol))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

                    if (!string.IsNullOrEmpty(requestTc.Server.Certificate))
                    {
                        string certPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, requestTc.Server.Certificate);
                        if (File.Exists(certPath))
                        {
                            try
                            {
                                X509Certificate cert = null;
                                if(string.IsNullOrEmpty(requestTc.Server.CertPw))// add pw
                                    cert = new X509Certificate(certPath);
                                else
                                    cert = new X509Certificate(certPath,requestTc.Server.CertPw);
                                request.ClientCertificates.Add(cert);
                            }
                            catch
                            {
                                return string.Empty;
                            }
                        }
                    }
                }

                Encoding encode = ParseEncoding(requestTc.Server.Encoding);
                using (Stream requestStream = request.GetRequestStream())
                using (StreamWriter requestStreamWriter = new StreamWriter(requestStream, encode))
                {
                    requestStreamWriter.Write(requestTc.Content);
                    requestStreamWriter.Close();
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader responseStreamReader = new StreamReader(responseStream, encode))
                {
                    strResult = responseStreamReader.ReadToEnd();
                }
                strResult = RemoveEscapeCharacter(strResult);
                
            }
            catch 
            {
                return string.Empty;
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                }
            }

            return strResult;
        }

        private static string RemoveEscapeCharacter(string str)
        {
            return str.Replace("&lt;", "<").Replace("&amp;", "&").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&apos;", "'");
        }


        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        private static bool ParseBool(string value)
        {
            if (value.Equals("true", StringComparison.OrdinalIgnoreCase))
                return true;
            else
                return false;
        }

        private static bool IsHttps(string str)
        {
            if (str.ToUpper().Equals("HTTPS", StringComparison.OrdinalIgnoreCase))
                return true;
            else
                return false;
        }

        private static Dictionary<string, string> GetHeader(string str)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string[] arr = str.Split(';');
            foreach (string item in arr)
            {
                string[] itemArr = item.Split('=');
                if (itemArr.Length == 2)
                {
                    dict.Add(itemArr[0], itemArr[1]);
                }
            }
            return dict;
        }

        private static Encoding ParseEncoding(string str)
        {
            if (str.Equals("utf-8", StringComparison.OrdinalIgnoreCase))
                return Encoding.UTF8;
            else if (str.Equals("unicode", StringComparison.OrdinalIgnoreCase))
                return Encoding.Unicode;
            else if (str.Equals("ascii", StringComparison.OrdinalIgnoreCase))
                return Encoding.ASCII;
            else if (str.Equals("utf-32", StringComparison.OrdinalIgnoreCase))
                return Encoding.UTF32;
            return Encoding.UTF8;
        }

    }
}
