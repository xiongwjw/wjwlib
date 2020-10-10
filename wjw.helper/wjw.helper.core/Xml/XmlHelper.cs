using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace wjw.helper.Xml
{
    public class XmlHelper
    {
        private XmlDocument _xmldoc = null;
        private string _path = string.Empty;

        public XmlHelper(string path)
        {
            try
            {
                _path = path;
                _xmldoc = new XmlDocument();
                _xmldoc.Load(path);
            }
            catch
            {
                _xmldoc = null;
            }

        }

        public void Save()
        {
            if (_xmldoc != null)
                _xmldoc.Save(_path);
        }

        public bool SetValue(string xPath,string attribute, string value)
        {
            if (_xmldoc == null)
                return false;
            XmlNode node =  _xmldoc.SelectSingleNode(xPath);
            if (node == null)
                return false;
            XmlAttribute attr = GetAttribute(node,attribute);
            if (attr == null)//create it and set value
                AddAttribute(node, attribute, value);
            else
                attr.Value = value;
            return true;
        }

        public string GetValue(string xPath,string attribute)
        {
            if (_xmldoc == null)
                return string.Empty;
            XmlNode node = _xmldoc.SelectSingleNode(xPath);
            if (node == null)
                return string.Empty;
            XmlAttribute attr = GetAttribute(node, attribute);
            if (attr == null)
                return string.Empty;
            else
                return attr.Value;

        }

        private void AddAttribute(XmlNode node,string name, string value)
        {
            XmlAttribute attr = _xmldoc.CreateAttribute(name);
            attr.Value = value;
            node.Attributes.Append(attr);
        }
        

        private XmlAttribute GetAttribute(XmlNode node, string name)
        {
            foreach (XmlAttribute attr in node.Attributes)
                if (attr.Name.Equals(name,StringComparison.OrdinalIgnoreCase))
                    return attr;
            return null;
        }
        

    }
}
