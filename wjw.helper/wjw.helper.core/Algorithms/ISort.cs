using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Algorithms
{
    /// <summary>
    /// 排序
    /// </summary>
    public interface ISort
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="input">待排序数组</param>
        /// <returns></returns>
        T[] Sort<T>(T[] input);
    }
}
