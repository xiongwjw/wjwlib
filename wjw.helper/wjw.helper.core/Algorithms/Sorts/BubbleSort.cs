using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Algorithms.Sorts
{
    /// <summary>
    /// 冒泡排序算法
    /// </summary>
    public class BubbleSort:ISort
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="input">待排序的数组</param>
        /// <returns></returns>
        public T[] Sort<T>(T[] input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                var key = input[i];
                var j = i - 1;
                while (j >= 0 && ((IComparable) input[j]).CompareTo(key) > 0)
                {
                    input[j + 1] = input[j];
                    j = j - 1;
                }
                input[j + 1] = key;
            }
            return input;
        }
    }
}
