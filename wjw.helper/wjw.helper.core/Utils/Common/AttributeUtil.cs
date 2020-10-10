using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Utils
{
    /// <summary>
    /// 属性操作工具类
    /// </summary>
    public class AttributeUtil
    {
        #region GetAttribute(获取属性)
        /// <summary>
        /// 获取属性信息
        /// </summary>
        /// <typeparam name="TAttribute">泛型属性</typeparam>
        /// <param name="memberInfo">元数据</param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(MemberInfo memberInfo)
        {
            return (TAttribute)memberInfo.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault();
        }

        /// <summary>
        /// 获取属性信息数组
        /// </summary>
        /// <typeparam name="TAttribute">泛型属性</typeparam>
        /// <param name="memberInfo">元数据</param>
        /// <returns></returns>
        public static TAttribute[] GetAttributes<TAttribute>(MemberInfo memberInfo)
        {
            return Array.ConvertAll(memberInfo.GetCustomAttributes(typeof(TAttribute), false), x => (TAttribute)x);
        }
        #endregion
    }
}
