using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace wjw.helper.Xml
{
    public class XMLOperator
    {
        //filenamekey_config/banksetting_networksetting

        private static Dictionary<string, XmlDocument> xmlDocuments = null;
        
        public static Dictionary<string, XmlDocument> XmlDocuments
        {
            get
            {
                if (xmlDocuments == null)
                {
                    xmlDocuments = new Dictionary<string, XmlDocument>();
                    foreach (var item in FileNames)
                    {
                        string fileNameItem =item.Value;
                        XmlDocument xdoc = new XmlDocument();
                        xdoc.Load(fileNameItem);
                        xmlDocuments.Add(item.Key, xdoc);
                    }
                }

                return xmlDocuments;
            }
        }

        private static Dictionary<string, string> fileNames = null;

        public static Dictionary<string, string> FileNames
        {
            get
            {
                return fileNames;
            }
        }

        public static void AddFile(string key, string filePath)
        {
            if (fileNames == null)
                fileNames = new Dictionary<string, string>();
            fileNames.Add(key, filePath);
        }

        public static string GetXmlAttValue(string pointPath)
        {
            string attValue = string.Empty;
            try
            {
                string[] points = pointPath.Split('_');
                if (points.Length < 3)
                    return string.Empty;
                string fileName = string.Empty;
                if (points.Length > 0)
                {
                    fileName = points[0];
                }
                Dictionary<string, XmlDocument> xmlDocs = XmlDocuments;

                if (xmlDocs.Count > 0)
                {
                    XmlDocument xmldoc = xmlDocs[fileName];
                    string xPath = points[1];
                    string attName = points[2];

                    XmlNode resultNode = xmldoc.SelectSingleNode(xPath);

                    if (resultNode != null)
                    {
                        foreach (XmlNode item in resultNode.ChildNodes)
                        {
                            if (item.Attributes != null)
                            {
                                string keyName = item.Attributes["key"].InnerText;
                                string keyName2 = item.Attributes["key"].Value;

                                if (keyName == attName)
                                {
                                    attValue = item.Attributes["value"].Value;
                                    return attValue;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return attValue;
        }

        public static bool SetXmlAttValue(string pointPath,string value)
        {
            string attValue = string.Empty;
            try
            {
                string[] points = pointPath.Split('_');
                if (points.Length < 3)
                    return false;
                string fileName = string.Empty;
                if (points.Length > 0)
                {
                    fileName = points[0];
                }
                Dictionary<string, XmlDocument> xmlDocs = XmlDocuments;

                if (xmlDocs.Count > 0)
                {
                    XmlDocument xmldoc = xmlDocs[fileName];
                    string xPath = points[1];
                    string attName = points[2];

                    XmlNode resultNode = xmldoc.SelectSingleNode(xPath);

                    if (resultNode != null)
                    {
                        foreach (XmlNode item in resultNode.ChildNodes)
                        {
                            if (item.Attributes != null)
                            {
                                string keyName = item.Attributes["key"].Value;//.InnerText ;
                                if (keyName == attName)
                                {
                                    item.Attributes["value"].Value=value;//.InnerText = value;                                    
                                    break;
                                }
                            }
                        }
                    }

                    xmldoc.Save(FileNames[fileName]);
                    return true;
                }
            }
            catch { }
            return false;
        }
    }
}
