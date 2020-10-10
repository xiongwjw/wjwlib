using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Utils
{
    /// <summary>
    /// 排序
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class OrderByAttribute : Attribute
    {
        /// <summary>
        /// 初始化一个<see cref="OrderByAttribute"/>类型的实例
        /// </summary>
        /// <param name="sortId">排序号</param>
        public OrderByAttribute(int sortId)
        {
            SortId = sortId;
        }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortId { get; set; }
    }
}
