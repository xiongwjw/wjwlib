
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper
{
    /// <summary>
    /// 空对象
    /// </summary>
    public interface INullObject
    {
        /// <summary>
        /// 是否空对象
        /// </summary>
        /// <returns></returns>
        bool IsNull();
    }

    /// <summary>
    /// 空对象
    /// </summary>
    public class NullObject:INullObject
    {
        /// <summary>
        /// 是否空对象
        /// </summary>
        /// <returns></returns>
        public bool IsNull()
        {
            return false;
        }
    }
}
