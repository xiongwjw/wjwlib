using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using wjw.helper.IO;

namespace wjw.helper.Base64
{
    public class Base64Helper
    {
        public static Bitmap ToImage(string imgDataBase64, string fileName="")
        {
            try
            {
                byte[] arr = Convert.FromBase64String(imgDataBase64);
                MemoryStream memStream = new MemoryStream(arr);
                Bitmap bitmap = new Bitmap(memStream);
                if(string.IsNullOrEmpty(fileName))
                    bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                memStream.Close();
                memStream.Dispose();
                return bitmap;
            }
            catch
            {
                return null;
            }
        }

        public static string FromImage(Bitmap bitmap)
        {
            try
            {
                MemoryStream memStream = new MemoryStream();
                bitmap.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[memStream.Length];
                memStream.Position = 0;
                memStream.Read(arr, 0, (int)memStream.Length);
                memStream.Close();
                bitmap.Dispose();
                return Convert.ToBase64String(arr);
            }
            catch
            {
                return string.Empty;
            }
        }


        public static string FromImageFile(string Imagefilename)
        {
            try
            {
                Bitmap bitmap = new Bitmap(Imagefilename);
                MemoryStream memStream = new MemoryStream();
                bitmap.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[memStream.Length];
                memStream.Position = 0;
                memStream.Read(arr, 0, (int)memStream.Length);
                memStream.Close();
                bitmap.Dispose();
                return Convert.ToBase64String(arr);
            }
            catch
            {
                return null;
            }
        }

        public static string FromFile(string filePath)
        {
            try
            {
                byte[] bytes =FileOperator.OpenByteFile(filePath);
                return Convert.ToBase64String(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool ToFile(string filePath,string content)
        {
            try
            {
                byte[] data = Convert.FromBase64String(content);
                FileOperator.SaveByteFile(data, filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string FromString(string source)
        {
            return FromString(Encoding.UTF8, source);
        }

        public static string FromString(Encoding encodeType, string source)
        {
            string encode = string.Empty;
            byte[] bytes = encodeType.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }

        public static string ToString(string result)
        {
            return ToString(Encoding.UTF8, result);
        }

        public static string ToString(Encoding encodeType, string result)
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encodeType.GetString(bytes);
            }
            catch
            {
                decode = result;
            }

            return decode;
        }

    }
}
