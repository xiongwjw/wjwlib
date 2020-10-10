using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace wjw.socket.Common
{
    public class FileHelper
    {
        public static byte[] ReadFile(string filePath)
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

        public static string ReadFileToText(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return string.Empty;
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool WriteTexttoFile(string content, string filePath)
        {
            try
            {
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
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


        public static bool WriteFile(byte[] data, string filePath)
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

        public static bool IsValidatePath(string path)
        {
            //to do
            return true;
        }

        public static string ReplaceDirectory(string fileFullName,string destPath)
        {
            return Path.Combine(destPath, Path.GetFileName(fileFullName));
        }

    }
}
