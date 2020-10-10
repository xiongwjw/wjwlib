using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Encrypts
{
    public class TripleDES
    {

        public static string Encrypt3Des(string aStrString, string aStrKey, CipherMode mode = CipherMode.CBC, string iv = "12345678")
        {
            try
            {
                var des = new TripleDESCryptoServiceProvider
                {
                    Key = Encoding.UTF8.GetBytes(aStrKey),
                    Mode = mode
                };
                if (mode == CipherMode.CBC)
                {
                    des.IV = Encoding.UTF8.GetBytes(iv);
                }
                var desEncrypt = des.CreateEncryptor();
                byte[] buffer = Encoding.UTF8.GetBytes(aStrString);
                return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }



        ///// <summary>
        ///// des 解密
        ///// </summary>
        ///// <param name="aStrString">加密的字符串</param>
        ///// <param name="aStrKey">密钥</param>
        ///// <param name="iv">解密矢量：只有在CBC解密模式下才适用</param>
        ///// <param name="mode">运算模式</param>
        ///// <returns>解密的字符串</returns>
        public static string Decrypt3Des(string aStrString, string aStrKey, CipherMode mode = CipherMode.ECB, string iv = "12345678")
        {
            try
            {
                var des = new TripleDESCryptoServiceProvider
                {
                    Key = Encoding.UTF8.GetBytes(aStrKey),
                    Mode = mode,
                    Padding = PaddingMode.PKCS7
                };
                if (mode == CipherMode.CBC)
                {
                    des.IV = Encoding.UTF8.GetBytes(iv);
                }
                var desDecrypt = des.CreateDecryptor();
                var result = "";
                byte[] buffer = Convert.FromBase64String(aStrString);
                result = Encoding.UTF8.GetString(desDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
                return result;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }


        //调用方法：

        //String desKey = "1dcrm4goRY8KODsgV1PPuHLB";//24位密钥
        //String desIv = "QCsJ2SKR";        ///8位向量

        //var request = DESEncrypt("123456789", desKey, desIv);//加密
        //var request = DESDecrypt("123456789", desKey, desIv);//解密

        /// <summary>
        ///3DES加密
        /// </summary>
        /// <param name="originalValue">加密数据</param>
        /// <param name="key">24位字符的密钥字符串</param>
        /// <param name="IV">8位字符的初始化向量字符串</param>
        /// <returns></returns>
        /// 
        private static readonly string desKey = "RY8KODsgV1PPuHLB";
        private static readonly string desIv = "QCsJ2SKR";
        public static string DESEncrypt(string originalValue, string key= "RY8KODsgV1PPuHLB", string IV= "QCsJ2SKR")
        {

            SymmetricAlgorithm sa;
            ICryptoTransform ct;
            System.IO.MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            sa = new TripleDESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ct = sa.CreateEncryptor();
            byt = Encoding.UTF8.GetBytes(originalValue);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }
        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="data">解密数据</param>
        /// <param name="key">24位字符的密钥字符串(需要和加密时相同)</param>
        /// <param name="iv">8位字符的初始化向量字符串(需要和加密时相同)</param>
        /// <returns></returns>
        public static string DESDecrypst(string data, string key = "RY8KODsgV1PPuHLB", string IV = "QCsJ2SKR")
        {
            SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
            mCSP.Key = Encoding.UTF8.GetBytes(key);
            mCSP.IV = Encoding.UTF8.GetBytes(IV);
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);
            byt = Convert.FromBase64String(data);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Encoding.UTF8.GetString(ms.ToArray());

        }


    }
}
