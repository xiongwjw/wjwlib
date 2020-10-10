using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Utils
{
    /// <summary>
    /// 序列化操作工具类
    /// </summary>
    public class SerializeUtil
    {
        #region ToBytes(将对象序列化到字节流中)
        /// <summary>
        /// 将对象序列化到字节流中
        /// </summary>
        /// <param name="instance">对象</param>
        /// <returns></returns>
        public static byte[] ToBytes(object instance)
        {
            if (instance == null)
            {
                return null;
            }
            BinaryFormatter serializer = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, instance);
                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }
        #endregion

        #region FromBytes(将字节流反序列化为对象)
        /// <summary>
        /// 将字节流反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类名</typeparam>
        /// <param name="buffer">字节流</param>
        /// <returns></returns>
        public static T FromBytes<T>(byte[] buffer) where T : class
        {
            if (buffer == null)
            {
                return default(T);
            }
            BinaryFormatter serializer = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0;
                object result = serializer.Deserialize(stream);
                if (result == null)
                {
                    return default(T);
                }
                return (T)result;
            }
        }
        #endregion
    }
}
