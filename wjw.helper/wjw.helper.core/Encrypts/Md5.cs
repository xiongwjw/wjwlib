using System;
using System.IO;
using System.Linq;
using System.Text;

namespace wjw.helper.Encrypts
{
    public class MD5
    {

        public static string CaculateStringMd5(string str,bool removeSeperater=true)
        {
            try
            {
                var md5 = System.Security.Cryptography.MD5.Create();
                string result = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(str)));
                if (removeSeperater)
                    result = result.Replace("-", "");
                return result;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string CaculateFileMd5(string path, bool removeSeperater = true)
        {
            try
            {
                using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var md5Provider = System.Security.Cryptography.MD5.Create();
                    byte[] hash_byte = md5Provider.ComputeHash(file);
                    string result = System.BitConverter.ToString(hash_byte);
                    if(removeSeperater)
                        result = result.Replace("-", "");
                    return result;
                }
            }
            catch
            {

                return string.Empty;

            }
        }
        
    }
}
