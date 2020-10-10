
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Xml.DynamicBuilder
{
    /// <summary>
    /// Xml动态生成器
    /// </summary>
    public static class XmlBuilder
    {
        /// <summary>
        /// 创建节点
        /// </summary>
        /// <returns></returns>
        public static dynamic Create()
        {
            return new ChildXmlNodesBuilder();
        }
    }
}
