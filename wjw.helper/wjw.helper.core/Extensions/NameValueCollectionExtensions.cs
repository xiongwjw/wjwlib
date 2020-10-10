using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wjw.helper.Text;

namespace wjw.helper.Extensions
{
    /// <summary>
    /// 键值对集合（NameValueCollection）扩展
    /// </summary>
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// 将键值对集合转换成字典
        /// </summary>
        /// <param name="source">键值对集合</param>
        /// <returns></returns>
        public static Dictionary<string, string> ToDictionary(this NameValueCollection source)
        {
            if (source != null)
            {
                Dictionary<string,string> dict=new Dictionary<string, string>();
                foreach (string key in source.AllKeys)
                {
                    dict.Add(key,source[key]);
                }
                return dict;
            }
            return null;
        }

        /// <summary>
        /// 将键值对集合转换成查询字符串
        /// </summary>
        /// <param name="source">键值对集合</param>
        /// <param name="valueFunc">值操作</param>
        /// <returns></returns>
        public static string ToQueryString(this NameValueCollection source,Func<NameValueCollection,string,string> valueFunc=null)
        {
            if (source != null)
            {
                Str sb=new Str();
                foreach (string key in source.AllKeys)
                {
                    if (valueFunc != null)
                    {
                        sb.Append("{0}={1}&", key, valueFunc(source, source[key]));
                    }
                    else
                    {
                        sb.Append("{0}={1}&", key, source[key]);
                    }                    
                }
                sb.RemoveEnd("&");
                return sb.ToString();
            }
            return string.Empty;
        }        
    }
}
