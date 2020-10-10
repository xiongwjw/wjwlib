using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Algorithms.Sorts
{
    /// <summary>
    /// 插入排序算法
    /// </summary>
    public class InsertionSort:ISort
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="input">待排序数组</param>
        /// <returns></returns>
        public T[] Sort<T>(T[] input)
        {
            for (int i = 1; i < input.Length; i++)
            {
                var key = input[i];
                int j = i-1;
                while ((j > 0) && ((IComparable) input[j]).CompareTo(key) > 0)
                {
                    input[j] = input[j - 1];
                    --j;
                }
                input[j] = key;
            }
            return input;
        }
    }
}
