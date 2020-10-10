using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace wjw.socket.Common
{
    public class Utility
    {
        public static int transactionIndex = 0;
        public static string GenSeriaNo()
        {
            transactionIndex++;
            if (transactionIndex > 999999)
                transactionIndex = 1;
            return transactionIndex.ToString().PadLeft(6, '0');
        }

        public static string GetMessageType(string data)
        {
            Regex re = new Regex("\"MessageType\":\"[A-Za-z]*?\"", RegexOptions.None);
            if (re.IsMatch(data))
            {
                string cmdLine = re.Match(data).Value;
                string[] cmdArr = cmdLine.Split(':');
                return cmdArr[1].Replace("\"", "");
            }
            else
                return string.Empty;
        }


    }
}
