using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace wjw.helper.Xml
{
    class XMLChecker
    {
        public static bool IsXml(string strXml)
        {
            string errorMsg = string.Empty;
            return IsXml(strXml, out errorMsg);
        }
          
        public static bool IsXml(string strXml, out string errorMessage)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(strXml);
                errorMessage = string.Empty;
                return true;
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
