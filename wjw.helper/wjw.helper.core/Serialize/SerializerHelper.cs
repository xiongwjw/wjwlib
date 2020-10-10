using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace wjw.helper.Serialize
{
    public class SerializerHelper
    {
        /// <summary>
        /// ��ָ���Ķ������л�ΪXML�ļ���������ļ�������ִ��״̬��
        /// </summary>
        /// <param name="o">Ҫ���л��Ķ���</param>
        /// <param name="path">����·��</param>
        /// <param name="isBinaryFile">���л������ɵ��ļ������Ƿ�Ϊ�������ļ���trueΪ�������ļ�������Ϊxml�ļ����ı��ļ�</param>
        /// <returns>����ִ��״̬</returns>
        public static bool Serialize(Object o, string path, bool isBinaryFile)
        {
            bool flag = false;
            try
            {
                if (isBinaryFile)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        formatter.Serialize(stream, o);
                        flag = true;
                    }
                }
                else
                {
                    XmlSerializer serializer = new XmlSerializer(o.GetType());
                    using (XmlTextWriter writer = new XmlTextWriter(path, Encoding.UTF8))
                    {
                        writer.Formatting = Formatting.Indented;
                        XmlSerializerNamespaces n = new XmlSerializerNamespaces();
                        n.Add("", "");
                        serializer.Serialize(writer, o, n);
                        flag = true;
                    }
                }
            }
            catch { flag = false; }
            return flag;
        }

        /// <summary>
        /// ��ָ���Ķ������л�ΪXML��ʽ���ַ��������ء�
        /// </summary>
        /// <param name="o">�����л��Ķ���</param>
        /// <returns>�������л�����ַ���</returns>
        public static string Serialize(Object o)
        {
            string xml = "";
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                using (MemoryStream mem = new MemoryStream())
                {
                    using (XmlTextWriter writer = new XmlTextWriter(mem, Encoding.UTF8))
                    {
                        writer.Formatting = Formatting.Indented;
                        XmlSerializerNamespaces n = new XmlSerializerNamespaces();
                        n.Add("", "");
                        serializer.Serialize(writer, o, n);

                        mem.Seek(0, SeekOrigin.Begin);
                        using (StreamReader reader = new StreamReader(mem))
                        {
                            xml = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch { xml = ""; }
            return xml;
        }

        /// <summary>
        /// ��ָ�����ļ��з����л�����Ӧ�Ķ��󲢷��ء�
        /// </summary>
        /// <param name="t">Ҫ�����л��Ķ�������</param>
        /// <param name="path">�ļ�·��</param>
        /// <param name="isBinaryFile">�����л����ļ������Ƿ�Ϊ�������ļ���trueΪ�������ļ�������Ϊxml�ļ����ı��ļ�</param>
        /// <returns>����Object</returns>
        public static object Deserialize(Type t, string path, bool isBinaryFile)
        {
            Object o = null;
            try
            {
                if (!isBinaryFile)
                {
                    XmlSerializer serializer = new XmlSerializer(t);
                    using (XmlTextReader reader = new XmlTextReader(path))
                    {
                        o = serializer.Deserialize(reader);
                    }
                }
                else
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        o = formatter.Deserialize(stream);
                    }
                }
            }
            catch { o = null; }
            return o;
        }

        /// <summary>
        /// ��ָ����xml��ʽ���ַ������л�Ϊ��Ӧ�Ķ��󲢷��ء�
        /// </summary>
        /// <param name="t">���������</param>
        /// <param name="xml">�������л���xml��ʽ���ַ�������</param>
        /// <returns>���ض�Ӧ�Ķ���</returns>
        public static Object Deserialize(Type t, string xml)
        {
            Object o = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(t);
                using (MemoryStream mem = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                {
                    o = serializer.Deserialize(mem);
                }
            }
            catch { o = null; }
            return o;
        }

        /// <summary>
        /// ��ָ���Ķ������л�ΪXML�ļ���������ִ��״̬��
        /// </summary>
        /// <param name="o">Ҫ���л��Ķ���</param>
        /// <param name="path">���ɵ��ļ�����</param>
        /// <returns>����ִ��״̬</returns>
        public static bool XmlSerialize(Object o, string path)
        {
            return SerializerHelper.Serialize(o, path, false);
        }

        /// <summary>
        /// ��ָ��XML�ļ��������л�Ϊ��Ӧ�Ķ��󲢷��ء�
        /// </summary>
        /// <param name="t">���������</param>
        /// <param name="path">XML�ļ�·��</param>
        /// <returns>���ض���</returns>
        public static Object XmlDeserialize(Type t, string path)
        {
            return SerializerHelper.Deserialize(t, path, false);
        }

        /// <summary>
        /// ��ָ���Ķ������л�Ϊ�������ļ���������ִ��״̬��
        /// </summary>
        /// <param name="o">Ҫ���л��Ķ���</param>
        /// <param name="path">���ɵ��ļ�����</param>
        /// <returns>����ִ��״̬</returns>
        public static bool BinarySerialize(Object o, string path)
        {
            return SerializerHelper.Serialize(o, path, true);
        }

        /// <summary>
        /// ��ָ���������ļ��������л�Ϊ��Ӧ�Ķ��󲢷��ء�
        /// </summary>
        /// <param name="t">���������</param>
        /// <param name="path">XML�ļ�·��</param>
        /// <returns>���ض���</returns>
        public static Object BinaryDeserialize(Type t, string path)
        {
            return SerializerHelper.Deserialize(t, path, true);
        }
    }
}