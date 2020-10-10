using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace wjw.helper.IO
{
    public class FileOperator
    {
        public static byte[] OpenByteFile(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    return data;
                }
            }
            catch
            {
                return null;
            }
            
        }

        public static bool SaveByteFile(byte[] data, string filePath)
        {
            try
            {
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                //be care need to have right in linux, will be override
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(data);
                    bw.Close();
                    fs.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string OpenTextFile(string filePath)
        {
            return OpenTextFile(filePath, Encoding.Default);
        }

        public static string OpenTextFile(string filePath,Encoding encoding)
        {
            try
            {
                if (!File.Exists(filePath))
                    return string.Empty;
                using (StreamReader sr = new StreamReader(filePath, encoding))
                {
                    return sr.ReadToEnd();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool SaveTextFile(string content, string filePath)
        {
            return SaveTextFile(content, filePath, Encoding.Default);
        }

        public static bool SaveTextFile(string content, string filePath, Encoding encoding)
        {
            try
            {
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                using (StreamWriter sw = new StreamWriter(filePath, false, encoding))
                {
                    sw.Write(content);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
