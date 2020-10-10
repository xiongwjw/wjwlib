using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wjw.helper.Json
{
    public class JsonFormater
    {
        public static void FormatJsonFile(string filePath)
        {
            string str = File.ReadAllText(filePath);
            string result = FormatJson(str);
            using (StreamWriter sr = new StreamWriter(filePath, false))
            {
                sr.Write(result);
                sr.Flush();
                sr.Close();
                sr.Dispose();
            }
        }


        public static string FormatJson(string str)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                TextReader tr = new StringReader(str);
                JsonTextReader jtr = new JsonTextReader(tr);
                object obj = serializer.Deserialize(jtr);
                if (obj != null)
                {
                    StringWriter textWriter = new StringWriter();
                    JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                    {
                        Formatting = Formatting.Indented,
                        Indentation = 4,
                        IndentChar = ' '
                    };
                    serializer.Serialize(jsonWriter, obj);
                    return textWriter.ToString();
                }
                else
                {
                    return str;
                }
            }
            catch
            {
                return str;
            }
           
        }
    }
}
