
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Extensions
{
    /// <summary>
    /// 颜色（Color）扩展
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// 转为RGB颜色
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>RGB颜色值</returns>
        public static string ToHtmlColor(this Color color)
        {
            return ColorTranslator.ToHtml(color);
        }
        /// <summary>
        /// 转为OLE颜色
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>OLE颜色值</returns>
        public static int ToOleColor(this Color color)
        {
            return ColorTranslator.ToOle(color);
        }
        /// <summary>
        /// 转为Windows颜色
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>Windows颜色值</returns>
        public static int ToWin32Color(this Color color)
        {
            return ColorTranslator.ToWin32(color);
        }
    }
}
