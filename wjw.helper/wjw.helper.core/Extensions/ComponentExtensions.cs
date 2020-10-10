
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Extensions
{
    /// <summary>
    /// 组件（Component）扩展
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// 判断目标组件是否处于设计模式
        /// </summary>
        /// <param name="target">组件</param>
        /// <returns>bool</returns>
        public static bool IsInDesignMode(this IComponent target)
        {
            var site = target.Site;
            return !ReferenceEquals(site, null) && site.DesignMode;
        }
        /// <summary>
        /// 判断目标组件是否不处于设计模式
        /// </summary>
        /// <param name="target">组件</param>
        /// <returns>bool</returns>
        public static bool IsInRuntimeMode(this IComponent target)
        {
            return !target.IsInDesignMode();
        }
    }
}
