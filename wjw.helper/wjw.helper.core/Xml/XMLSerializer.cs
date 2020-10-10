using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace wjw.helper.Xml
{
    public class XMLSerializer
    {

        public static string XmlSerialize<T>(T obj)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";
            settings.NewLineChars = "\r\n";
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;

            using (TextWriter tw = new StringWriter())
            using (XmlWriter xmlWriter = XmlWriter.Create(tw, settings))
            {
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);
                xs.Serialize(xmlWriter, obj, namespaces);
                return tw.ToString();
            }

        }

        public static string XmlSerialize(Type t, object obj)
        {
            XmlSerializer xs = new XmlSerializer(t);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";
            settings.NewLineChars = "\r\n";
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;

            using (TextWriter tw = new StringWriter())
            using (XmlWriter xmlWriter =XmlWriter.Create(tw,settings))
            {
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);
                xs.Serialize(xmlWriter, obj, namespaces);
                return tw.ToString();
            }
        }

        public static T XmlDeserialize<T>(string xml) where T : class
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (TextReader tr = new StringReader(xml))
            {
                return xs.Deserialize(tr) as T;
            }
        }

        public static object XmlDeserialize(Type t , string xml)
        {
            XmlSerializer xs = new XmlSerializer(t);
            using (TextReader tr = new StringReader(xml))
            {
                return xs.Deserialize(tr);
            }
        }

    }
}
