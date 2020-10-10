
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Algorithms.Trees
{
    /// <summary>
    /// 构建子树节点上下文
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class BuildChildrenContext<T>
    {
        /// <summary>
        /// 初始化一个<see cref="BuildChildrenContext{T}"/>类型的实例
        /// </summary>
        /// <param name="tree">树节点</param>
        public BuildChildrenContext(TreeNode tree)
        {
            this.Tree = tree;
        }

        /// <summary>
        /// 当前树节点
        /// </summary>
        public TreeNode Tree { get; }

        /// <summary>
        /// 设置节点集合并返回子树节点上下文
        /// </summary>
        /// <typeparam name="V">实体类型</typeparam>
        /// <param name="itemSelector">节点集合选择器</param>
        /// <param name="textSelect">文本选择器</param>
        /// <returns></returns>
        public BuildChildrenContext<V> SetItems<V>(Func<T, IEnumerable<V>> itemSelector,
            Func<V, string> textSelect = null)
        {
            var leafNodes = Tree.GetLeafNodes().OfType<TreeNode<T>>();
            foreach (TreeNode<T> leafNode in leafNodes)
            {
                foreach (var child in itemSelector(leafNode.Value))
                {
                    var node = TreeBuilder.BuildNode(child, textSelect);
                    leafNode.Add(node);
                }
            }
            return new BuildChildrenContext<V>(Tree);
        }
        /// <summary>
        /// 递归设置节点集合并返回子树节点上下文
        /// </summary>
        /// <param name="itemSelector">节点集合选择器</param>
        /// <param name="textSelect">文本选择器</param>
        /// <returns></returns>
        public BuildChildrenContext<T> SetRecursiveItems(Func<T, IEnumerable<T>> itemSelector,
            Func<T, string> textSelect = null)
        {
            var context = this;
            while (Tree.GetLeafNodes().OfType<TreeNode<T>>().Any(n => itemSelector(n.Value).Any()))
            {
                context = context.SetItems<T>(itemSelector, textSelect);
            }
            return context;
        }
    }
}
