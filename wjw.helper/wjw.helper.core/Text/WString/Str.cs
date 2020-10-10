
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Text
{
    /// <summary>
    /// 字符串操作
    /// </summary>
    public sealed partial class Str
    {
        #region Field(字段)
        /// <summary>
        /// 字符串生成器
        /// </summary>
        private StringBuilder Builder { get; set; }
        #endregion

        #region Property(属性)
        /// <summary>
        /// 字符串长度
        /// </summary>
        public int Length
        {
            get { return Builder.Length; }
        }
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="Str"/>类型的实例
        /// </summary>
        public Str()
        {
            Builder = new StringBuilder();
        }

        /// <summary>
        /// 初始化一个<see cref="Str"/>类型的实例
        /// </summary>
        /// <param name="length">起始大小</param>
        public Str(int length)
        {            
            Builder=new StringBuilder(length);
        }
        #endregion
        
        #region Append(追加内容)
        /// <summary>
        /// 追加内容
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="value">值</param>
        public void Append<T>(T value)
        {
            Builder.Append(value);
        }

        /// <summary>
        /// 追加内容
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="args">参数</param>
        public void Append(string value, params object[] args)
        {
            if (args == null)
            {
                args = new object[] { string.Empty };
            }
            if (args.Length == 0)
            {
                Builder.Append(value);
            }
            else
            {
                Builder.AppendFormat(value, args);
            }
        }
        #endregion

        #region AppendLine(追加内容并换行)
        /// <summary>
        /// 追加内容并换行
        /// </summary>
        public void AppendLine()
        {
            Builder.AppendLine();
        }

        /// <summary>
        /// 追加内容并换行
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="value">值</param>
        public void AppendLine<T>(T value)
        {
            Append(value);
            AppendLine();
        }

        /// <summary>
        /// 追加内容并换行
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="args">参数</param>
        public void AppendLine(string value, params object[] args)
        {
            Append(value, args);
            AppendLine();
        }
        #endregion

        #region Replace(替换内容)
        /// <summary>
        /// 替换内容
        /// </summary>
        /// <param name="value">值</param>
        public void Replace(string value)
        {
            Builder.Clear();
            Builder.Append(value);
        }
        #endregion

        #region RemoveEnd(移除末尾字符串)
        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="end">末尾字符串</param>
        public void RemoveEnd(string end)
        {
            string result = Builder.ToString();
            if (!result.EndsWith(end))
            {
                return;
            }
            int index = result.LastIndexOf(end, StringComparison.Ordinal);
            Builder = Builder.Remove(index, end.Length);
        }
        #endregion

        #region Clear(清空字符串)
        /// <summary>
        /// 清空字符串
        /// </summary>
        public void Clear()
        {
            Builder = Builder.Clear();
        }
        #endregion

        #region ToString(转换为字符串)
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Builder.ToString();
        }
        #endregion
        
    }
}
