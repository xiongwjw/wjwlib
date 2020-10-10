
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Algorithms.Trees
{
    /// <summary>
    /// 树 生成器
    /// </summary>
    public class TreeBuilder
    {
        /// <summary>
        /// 生成根节点
        /// </summary>
        /// <param name="text">根节点名</param>
        /// <returns></returns>
        public static BuildRootContext Build(string text)
        {
            var root = new TreeNode(text);
            return new BuildRootContext(root);
        }

        /// <summary>
        /// 生成树节点
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="t">实体</param>
        /// <param name="textSelect">文本选择器</param>
        /// <returns></returns>
        internal static TreeNode<T> BuildNode<T>(T t, Func<T, string> textSelect = null)
        {
            var text = textSelect != null ? textSelect(t) : Convert.ToString(t);
            var node = new TreeNode<T>(text, t);
            return node;
        }
    }
}
