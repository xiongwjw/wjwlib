
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Algorithms.Trees
{
    /// <summary>
    /// 泛型树节点
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class TreeNode<T> : TreeNode
    {
        /// <summary>
        /// 初始化一个<see cref="TreeNode{T}"/>类型的实例
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        public TreeNode(string text, object value = null) : base(text, value)
        {
        }

        /// <summary>
        /// 值
        /// </summary>
        public new T Value
        {
            get { return (T)base.Value; }
        }
    }
}
