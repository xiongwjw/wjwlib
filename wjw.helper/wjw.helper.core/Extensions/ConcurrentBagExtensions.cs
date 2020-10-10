
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Extensions
{
    /// <summary>
    /// 线程安全的无序集合（ConcurrentBag）扩展
    /// </summary>
    public static partial class ConcurrentBagExtensions
    {
        /// <summary>
        /// 清除对象的数据
        /// </summary>
        /// <typeparam name="TEntity">对象类型</typeparam>
        /// <param name="list">线程安全的无序集合</param>
        public static void Clear<TEntity>(this ConcurrentBag<TEntity> list)
        {
            TEntity entity;
            while (list.TryTake(out entity))
            {                
            }
        }
    }
}
