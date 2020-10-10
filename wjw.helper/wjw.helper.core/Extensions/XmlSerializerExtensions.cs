
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace wjw.helper.Extensions
{
    /// <summary>
    /// XML序列化（XML）扩展
    /// </summary>
    public static class XmlSerializerExtensions
    {
        #region CanXmlSerialize(检查能否Xml序列化实例)
        /// <summary>
        /// 检查能否Xml序列化实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="instance">实例</param>
        /// <returns>bool</returns>
        public static bool CanXmlSerialize<T>(this T instance) where T : class, new()
        {
            try
            {
                var stream = new MemoryStream();
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, instance);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region CanXmlDeserialize(检查能否Xml反序列化文件)
        /// <summary>
        /// 检查能否Xml反序列化文件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="fileName">文件名</param>
        /// <returns>bool</returns>
        public static bool CanXmlDeserialize<T>(this string fileName) where T : class, new()
        {
            return CanXmlDeserialize<T>(new FileStream(fileName, FileMode.Open, FileAccess.Read));
        }
        /// <summary>
        /// 检查能否Xml反序列化文件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="stream">字节流</param>
        /// <returns>bool</returns>
        public static bool CanXmlDeserialize<T>(this Stream stream) where T : class, new()
        {
            try
            {
                var reader = XmlReader.Create(stream);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return serializer.CanDeserialize(reader);
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region XmlSerialize(Xml序列化)
        /// <summary>
        /// Xml序列化实例到流中
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="instance">实例</param>
        /// <param name="stream">字节流</param>
        public static void XmlSerialize<T>(this T instance, Stream stream) where T : class, new()
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, instance);
            //返回到文件开始位置
            stream.Position = 0;
        }
        /// <summary>
        /// Xml序列化实例到文件中
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="instance">实例</param>
        /// <param name="fileName">文件名</param>
        public static void XmlSerizlize<T>(this T instance, string fileName) where T : class, new()
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(stream, instance);
            }
        }
        #endregion
        #region XmlDeserialize(Xml反序列化)
        /// <summary>
        /// Xml反序列化当前流对象到实例流中
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="stream">字节流</param>
        /// <returns>对象</returns>
        public static T XmlDeserialize<T>(this Stream stream) where T : class, new()
        {
            stream.Position = 0;
            var serializer = new XmlSerializer(typeof(T));
            return serializer.Deserialize(stream) as T;
        }
        #endregion
    }
}
