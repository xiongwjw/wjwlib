
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Encrypts
{
    /// <summary>
    /// RSA加密算法，可用于数据加密或者数字签名
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class RSACrypt
    {
        #region Constructor(构造函数)
        /// <summary>
        /// 构造函数，空
        /// </summary>
        public RSACrypt()
        {

        }
        #endregion

        #region RsaKey(生成RSA密匙)
        /// <summary>
        /// 生成RSA密匙
        /// </summary>
        /// <param name="xmlKey">密匙</param>
        /// <param name="xmlPublicKey">公有密匙</param>
        public void RsaKey(out string xmlKey, out string xmlPublicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            xmlKey = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }
        #endregion
    }
}
