﻿
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wjw.helper.Extensions;

namespace wjw.helper.Logging
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    public static class LogManager
    {
        #region Field(字段)
        /// <summary>
        /// 缓存日志字典
        /// </summary>
        private static readonly ConcurrentDictionary<string, ILogger> Loggers;
        /// <summary>
        /// 对象锁
        /// </summary>
        private static readonly object LockObj = new object();
        #endregion

        #region Property(属性)
        /// <summary>
        /// 获取日志适配器集合
        /// </summary>
        internal static ICollection<ILoggerAdapter> Adapters { get; private set; }
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="LogManager"/>类型的实例
        /// </summary>
        static LogManager()
        {
            Loggers = new ConcurrentDictionary<string, ILogger>();
            Adapters = new List<ILoggerAdapter>();
        }
        #endregion

        #region AddLoggerAdapter(添加日志适配器)
        /// <summary>
        /// 添加日志适配器
        /// </summary>
        /// <param name="adapter">日志适配器</param>
        public static void AddLoggerAdapter(ILoggerAdapter adapter)
        {
            lock (LockObj)
            {
                if (Adapters.Any(m => m == adapter))
                {
                    return;
                }
                Adapters.Add(adapter);
                Loggers.Clear();
            }
        }
        #endregion

        #region RemoveLoggerAdapter(移除日志适配器)
        /// <summary>
        /// 移除日志适配器
        /// </summary>
        /// <param name="adapter">日志适配器</param>
        public static void RemoveLoggerAdapter(ILoggerAdapter adapter)
        {
            lock (LockObj)
            {
                if (Adapters.All(m => m != adapter))
                {
                    return;
                }
                Adapters.Remove(adapter);
                Loggers.Clear();
            }
        }
        #endregion

        #region SetEntryInfo(设置日志记录入口参数)
        /// <summary>
        /// 设置日志记录入口参数
        /// </summary>
        /// <param name="enabled">是否允许记录日志，如为false，将完全禁止日志记录</param>
        /// <param name="entryLevel">日志级别的入口控制，级别决定是否执行相应级别的日志记录功能</param>
        public static void SetEntryInfo(bool enabled, LogLevel entryLevel)
        {
            InternalLogger.EntryEnabled = enabled;
            InternalLogger.EntryLogLevel = entryLevel;
        }
        #endregion

        #region GetLogger(获取日志记录器实例)
        /// <summary>
        /// 获取日志记录器实例
        /// </summary>
        /// <param name="name">类型名</param>
        /// <returns></returns>
        public static ILogger GetLogger(string name)
        {
          //  name.CheckNotNullOrEmpty("name");
            lock (LockObj)
            {
                ILogger logger;
                if (Loggers.TryGetValue(name, out logger))
                {
                    return logger;
                }
                logger = new InternalLogger(name);
                Loggers[name] = logger;
                return logger;
            }
        }

        /// <summary>
        /// 获取指定类型的日志记录器实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static ILogger GetLogger(Type type)
        {
            //type.CheckNotNull("type");
            return GetLogger(type.FullName);
        }

        /// <summary>
        /// 获取指定类型的日志记录器实例
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public static ILogger GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }
        #endregion

    }
}
