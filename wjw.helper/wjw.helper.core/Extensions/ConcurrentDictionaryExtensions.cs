
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Extensions
{
    /// <summary>
    /// ConcurrentDictionary线程安全集合扩展
    /// </summary>
    public static class ConcurrentDictionaryExtensions
    {
        #region Remove(移除字典项)
        /// <summary>
        /// 移除字典项，指定键
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dictionary">字典</param>
        /// <param name="key">键</param>
        public static void Remove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            dictionary.TryRemove(key, out value);
        }
        #endregion
        
    }
}
