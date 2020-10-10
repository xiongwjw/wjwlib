
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Extensions
{
    /// <summary>
    /// 文本读取器（TextReader）扩展
    /// </summary>
    public static class TextReaderExtensions
    {
        /// <summary>
        /// 通用文本迭代器
        /// </summary>
        /// <param name="reader">文本读取器</param>
        /// <returns></returns>
        /// <example>
        /// 	<code>
        /// 		using(var reader = fileInfo.OpenText()) {
        /// 		foreach(var line in reader.IterateLines()) {
        /// 		// ...
        /// 		}
        /// 		}
        /// 	</code>
        /// </example>
        public static IEnumerable<string> IterateLine(this TextReader reader)
        {
            string line = null;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
        /// <summary>
        /// 执行通用文本迭代器，（传递委托/Lambda表达式）
        /// </summary>
        /// <param name="reader">文本读取器</param>
        /// <param name="action">委托/Lambda表达式</param>
        /// <example>
        /// 	<code>
        /// 		using(var reader = fileInfo.OpenText()) {
        /// 		reader.IterateLines(l => Console.WriteLine(l));
        /// 		}
        /// 	</code>
        /// </example>
        public static void IterateLines(this TextReader reader, Action<string> action)
        {
            foreach (var line in reader.IterateLine())
            {
                action(line);
            }
        }
    }
}
