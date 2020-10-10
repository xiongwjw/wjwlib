
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Exceptions
{
    /// <summary>
    /// Jce框架异常类
    /// </summary>
    [Serializable]
    public class JceException : Exception
    {
        /// <summary>
        /// 初始化<see cref="JceException"/>类的新实例
        /// </summary>
        public JceException()
        { }

        /// <summary>
        /// 使用指定错误消息初始化<see cref="JceException"/>类的新实例。
        /// </summary>
        /// <param name="message">描述错误的消息</param>
        public JceException(string message)
            : base(message)
        { }
        /// <summary>
        /// 获取内部异常数组
        /// </summary>
        public Exception[] InnerExceptions { get; protected set; }
        /// <summary>
        /// 初始化一个新的实例
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerExceptions">内部异常数组</param>
        public JceException(string message, Exception[] innerExceptions)
            : base(message)
        {
            InnerExceptions = innerExceptions;
        }

        /// <summary>
        /// 使用异常消息与一个内部异常实例化一个<see cref="JceException"/>类的新实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="inner">用于封装在<see cref="JceException"/>内部的异常实例</param>
        public JceException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// 使用可序列化数据实例化一个<see cref="JceException"/>类的新实例
        /// </summary>
        /// <param name="info">保存序列化对象数据的对象。</param>
        /// <param name="context">有关源或目标的上下文信息。</param>
        protected JceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
        /// <summary>
        /// 合并异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerExceptions">内部异常数组</param>
        /// <returns>合并后的异常</returns>
        public static Exception Combine(string message, params Exception[] innerExceptions)
        {
            if (innerExceptions.Length == 1)
            {
                return innerExceptions[0];
            }
            return new JceException(message, innerExceptions);
        }
        /// <summary>
        /// 合并指定的异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerExceptions">内部异常集合</param>
        /// <returns>合并后的异常</returns>
        public static Exception Combine(string message, IEnumerable<Exception> innerExceptions)
        {
            return Combine(message, innerExceptions.ToArray());
        }
    }
}
