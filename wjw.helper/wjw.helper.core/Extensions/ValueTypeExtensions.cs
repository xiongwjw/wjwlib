
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Extensions
{
    /// <summary>
    /// 泛型值类型扩展
    /// </summary>
    public static class ValueTypeExtensions
    {
        /// <summary>
        /// 确定指定值是否为空
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <returns>bool</returns>
        public static bool IsEmpty<T>(this T value) where T : struct
        {
            return value.Equals(default(T));
        }
        /// <summary>
        /// 确定指定值是否不为空
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <returns>bool</returns>
        public static bool IsNotEmpty<T>(this T value) where T : struct
        {
            return (value.IsEmpty() == false);
        }
        /// <summary>
        ///  将指定值转换为相应的可空类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <returns>可空类型</returns>
        public static T? ToNullable<T>(this T value) where T : struct
        {
            return (value.IsEmpty() ? null : (T?)value);
        }
    }
}
