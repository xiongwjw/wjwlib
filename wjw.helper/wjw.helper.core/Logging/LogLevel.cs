
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Logging
{
    /// <summary>
    /// 日志级别，表示日志输出级别的枚举
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 输出所有级别的日志
        /// </summary>
        All = 0,
        /// <summary>
        /// 输出表示跟踪的日志级别
        /// </summary>
        Trace = 1,
        /// <summary>
        /// 输出表示调试的日志级别
        /// </summary>
        Debug = 2,
        /// <summary>
        /// 输出表示消息的日志级别
        /// </summary>
        Info = 3,
        /// <summary>
        /// 输出表示警告的日志级别
        /// </summary>
        Warn = 4,
        /// <summary>
        /// 输出表示错误的日志级别
        /// </summary>
        Error = 5,
        /// <summary>
        /// 输出表示严重错误的日志级别
        /// </summary>
        Fatal = 6,
        /// <summary>
        /// 关闭所有日志，不输出日志
        /// </summary>
        Off = 7
    }
}
