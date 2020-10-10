
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Algorithms.Trees
{
    /// <summary>
    /// 生成根树节点上下文
    /// </summary>
    public class BuildRootContext
    {
        /// <summary>
        /// 初始化一个<see cref="BuildRootContext"/>类型的实例
        /// </summary>
        /// <param name="tree">树节点</param>
        public BuildRootContext(TreeNode tree)
        {
            this.Tree = tree;
        }

        /// <summary>
        /// 当前树节点
        /// </summary>
        public TreeNode Tree { get; }

        /// <summary>
        /// 设置节点集合并返回子树内容
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="items">节点集合</param>
        /// <returns></returns>
        public BuildChildrenContext<T> SetItems<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                var node = TreeBuilder.BuildNode(item);
                Tree.Add(node);
            }
            return new BuildChildrenContext<T>(Tree);
        }
    }
}
