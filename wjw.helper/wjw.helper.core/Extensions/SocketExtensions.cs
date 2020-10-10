
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Extensions
{
    /// <summary>
    /// 套接字接口（Socket）扩展
    /// </summary>
    public static class SocketExtensions
    {
        /// <summary>
        /// 是否已连接
        /// </summary>
        /// <param name="socket">套接字</param>
        /// <returns>bool</returns>
        public static bool IsConnected(this Socket socket)
        {
            var part1 = socket.Poll(1000, SelectMode.SelectRead);
            var part2 = (socket.Available == 0);
            return part1 & part2;
        }
    }
}
