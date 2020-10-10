using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Algorithms.Sorts
{
    /// <summary>
    /// 选择排序算法
    /// </summary>
    public class SelectionSort:ISort
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="input">待排序数组</param>
        /// <returns></returns>
        public T[] Sort<T>(T[] input)
        {             
            for (int i = 0; i < input.Length-1; i++)
            {
                var min = i;
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (((IComparable) input[j]).CompareTo(input[min]) < 0)
                    {
                        min = j;
                    }                    
                }
                T temp = input[min];
                input[min] = input[i];
                input[i] = temp;
            }
            return input;
        }
    }
}
