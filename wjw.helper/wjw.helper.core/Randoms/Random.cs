
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Randoms
{
    /// <summary>
    /// 随机数操作
    /// </summary>
    public class Random
    {
        #region Field(字段)
        /// <summary>
        /// 随机数
        /// </summary>
        private readonly System.Random _random;
        #endregion

        #region Construct(构造函数)
        /// <summary>
        /// 初始化随机数
        /// </summary>
        public Random()
        {
            _random = new System.Random();
        }

        /// <summary>
        /// 初始化随机数
        /// </summary>
        /// <param name="seed">种子数</param>
        public Random(int seed)
        {
            _random=new System.Random(seed);
        }
        #endregion

        #region GetInt(获取指定范围的随机整数，该范围包括最小值，但不包括最大值)
        /// <summary>
        /// 获取指定范围的随机整数，该范围包括最小值，但不包括最大值
        /// </summary>
        /// <param name="minNum">最小值</param>
        /// <param name="maxNum">最大值</param>
        /// <returns></returns>
        public int GetInt(int minNum, int maxNum)
        {
            return _random.Next(minNum, maxNum);
        }

        /// <summary>
        /// 获取随机整数
        /// </summary>
        /// <returns></returns>
        public int GetInt()
        {
            return _random.Next();
        }
        #endregion

        #region GetDouble(获取一个介于0.0和1.0之间的随机数)
        /// <summary>
        /// 获取一个介于0.0和1.0之间的随机数
        /// </summary>
        /// <returns></returns>
        public double GetDouble()
        {
            return _random.NextDouble();
        }
        #endregion

        #region GetSortList(获取随机排序的集合)
        /// <summary>
        /// 获取随机排序的集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <returns></returns>
        public static List<T> GetSortList<T>(IEnumerable<T> array)
        {
            if (array == null)
            {
                return null;
            }
            List<T> list = array.ToList();
            Random random = new Random();
            for (int i = 0; i < list.Count; i++)
            {
                int position1 = random.GetInt(0, list.Count);
                int position2 = random.GetInt(0, list.Count);
                T temp = list[position1];
                list[position1] = list[position2];
                list[position2] = temp;
            }
            return list;
        }
        #endregion
    }
}
