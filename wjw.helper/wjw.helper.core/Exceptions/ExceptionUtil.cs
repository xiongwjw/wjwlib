
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Exceptions
{
    /// <summary>
    /// 异常工具类
    /// </summary>
    public class ExceptionUtil
    {
        #region IgnoreException(忽略异常)
        /// <summary>
        /// 忽略异常
        /// </summary>
        /// <param name="action">操作</param>
        public static void IgnoreException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 忽略异常
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="action">操作</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T IgnoreException<T>(Func<T> action, T defaultValue = default(T))
        {
            try
            {
                return action();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        #endregion
    }
}
