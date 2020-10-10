
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Extensions
{
    /// <summary>
    /// 扩展方法设置
    /// </summary>
    public class ExtensionMethodSetting
    {
        /// <summary>
        /// 初始化扩展方法设置类的静态实例
        /// </summary>
        static ExtensionMethodSetting()
        {
            DefaultEncoding = Encoding.UTF8;
            DefaultCulture = CultureInfo.CurrentCulture;
        }
        /// <summary>
        /// 默认编码
        /// </summary>
        public static Encoding DefaultEncoding { get; set; }
        /// <summary>
        /// 默认区域设置
        /// </summary>
        public static CultureInfo DefaultCulture { get; set; }
    }
}
