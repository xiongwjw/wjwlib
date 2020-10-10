﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using wjw.helper.Utils;

namespace wjw.helper
{
    /// <summary>
    /// 系统操作
    /// </summary>
    public class Sys
    {
        #region Line(换行符)
        /// <summary>
        /// 换行符
        /// </summary>
        public static string Line
        {
            get { return Environment.NewLine; }
        }
        #endregion

        #region Guid(全局唯一标识符)
        /// <summary>
        /// 全局唯一标识符
        /// </summary>
        public static Guid Guid
        {
            get { return Guid.NewGuid(); }
        }
        #endregion

        #region GetType(获取类型)
        /// <summary>
        /// 获取类型,对可空类型进行处理
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public static Type GetType<T>()
        {
            return Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        }
        #endregion


        #region CurrentAppDomain(获取当前应用程序域)
        /// <summary>
        /// 获取当前应用程序域
        /// </summary>
        public static AppDomain CurrentAppDomain
        {
            get { return Thread.GetDomain(); }
        }
        #endregion

        #region Clone(克隆对象)
        /// <summary>
        /// 使用序列化机制克隆一个对象
        /// </summary>
        /// <typeparam name="T">原始对象的类名</typeparam>
        /// <param name="instance">原始对象实例</param>
        /// <returns></returns>
        public static T Clone<T>(T instance) where T : class
        {
            byte[] buffer = SerializeUtil.ToBytes(instance);
            return SerializeUtil.FromBytes<T>(buffer);
        }
        #endregion

        #region GetMethodName(获取指定调用层级的方法名)
        /// <summary>
        /// 获取指定调用层级的方法名
        /// </summary>
        /// <param name="level">调用层级</param>
        /// <returns></returns>
        public static string GetMethodName(int level)
        {
            //创建一个堆栈跟踪
            StackTrace trace = new StackTrace();
            //获取指定调用层级的方法名
            return trace.GetFrame(level).GetMethod().Name;
        }
        #endregion
    }
}
