using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Encrypts
{
    /// <summary>
    /// HMAC-SHA1（Hash-based Message Authentication Code）算法
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class HmacSHA1Crypt
    {
        #region Encrypt(加密)
        /// <summary>
        /// 加密并使用Base64进行转换
        /// </summary>
        /// <param name="data">需要加密的字符串</param>
        /// <param name="key">密匙</param>
        /// <param name="encoding">编码类型</param>
        /// <returns></returns>
        public static string EncryptBase64(string data, string key, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            var bytes = encoding.GetBytes(data);
            var keyBytes = encoding.GetBytes(key);
            byte[] resultBytes = Encrypt(keyBytes, bytes);
            return Convert.ToBase64String(resultBytes);
        }

        /// <summary>
        /// 加密并返回UTF8编码的字符串
        /// </summary>
        /// <param name="data">需要加密的字符串</param>
        /// <param name="key">密匙</param>
        /// <param name="encoding">编码类型</param>
        /// <returns></returns>
        public static string EncryptUtf8(string data, string key, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            var bytes = encoding.GetBytes(data);
            var keyBytes = encoding.GetBytes(key);
            byte[] resultBytes = Encrypt(keyBytes, bytes);
            return Encoding.UTF8.GetString(resultBytes);
        }

        #endregion

        #region Private Methods(私有方法)
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="key">密匙</param>
        /// <param name="bytes">需要加密的字节流</param>
        /// <returns></returns>
        private static byte[] Encrypt(byte[] key, byte[] bytes)
        {
            byte[] resultBytes;
            using (HMACSHA1 hmac = new HMACSHA1(key))
            {
                resultBytes = hmac.ComputeHash(bytes);
            }
            return resultBytes;
        }
        #endregion

    }
}
