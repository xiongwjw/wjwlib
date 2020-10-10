
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Extensions
{
    /// <summary>
    /// 异常（Exception）扩展
    /// </summary>
    public static class ExceptionExtensions
    {
        #region FormatMessage(格式化异常消息)
        /// <summary>
        /// 格式化异常消息
        /// </summary>
        /// <param name="e">异常对象</param>
        /// <param name="isHideStackTrace">是否隐藏异常规模信息</param>
        /// <returns>格式化后的异常信息字符串</returns>
        public static string FormatMessage(this Exception e, bool isHideStackTrace = false)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            string appString = string.Empty;
            while (e != null)
            {
                if (count > 0)
                {
                    appString += "  ";
                }
                sb.AppendLine(string.Format("{0}异常消息：{1}", appString, e.Message));
                sb.AppendLine(string.Format("{0}异常类型：{1}", appString, e.GetType().FullName));
                sb.AppendLine(string.Format("{0}异常方法：{1}", appString, (e.TargetSite == null ? null : e.TargetSite.Name)));
                sb.AppendLine(string.Format("{0}异常源：{1}", appString, e.Source));
                if (!isHideStackTrace && e.StackTrace != null)
                {
                    sb.AppendLine(string.Format("{0}异常堆栈：{1}", appString, e.StackTrace));
                }
                if (e.InnerException != null)
                {
                    sb.AppendLine(string.Format("{0}内部异常：", appString));
                    count++;
                }
                e = e.InnerException;
            }
            return sb.ToString();
        }
        #endregion
        #region GetOriginalException(获取原始异常)
        /// <summary>
        /// 获取原始异常
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        [Obsolete("Use GetBaseException instead")]
        public static Exception GetOriginalException(this Exception exception)
        {
            if (exception.InnerException == null)
            {
                return exception;
            }
            return exception.InnerException.GetOriginalException();
        }
        #endregion
        #region Messages(获取所有错误消息列表)
        /// <summary>
        /// 获取所有错误消息列表
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        /// <note>
        /// 最内部的异常消息在列表中第一项，最外层的异常消息在列表中最后一项
        /// </note>
        public static IEnumerable<string> Messages(this Exception exception)
        {
            return exception != null
                ? new List<string>(exception.InnerException.Messages()) { exception.Message }
                : Enumerable.Empty<string>();
        }
        #endregion
        #region Exceptions(获取所有异常列表)
        /// <summary>
        /// 获取所有异常列表
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        public static IEnumerable<Exception> Exceptions(this Exception exception)
        {
            return exception != null
                ? new List<Exception>(exception.InnerException.Exceptions
                    ()) { exception }
                : Enumerable.Empty<Exception>();
        }
        #endregion
    }
}
