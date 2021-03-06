﻿using System.Reflection;

namespace wjw.helper.Reflection
{
    /// <summary>
    /// 反射 操作
    /// </summary>
    public static partial class Reflections
    {
        #region GetCurrentAssemblyName(获取当前程序集名称)

        /// <summary>
        /// 获取当前程序集名称
        /// </summary>
        public static string GetCurrentAssemblyName() => Assembly.GetCallingAssembly().GetName().Name;

        #endregion
    }
}
