using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace MyProgram.GlobalFuncs
{
    public class XmlOperator
    {
        private static XmlOperator instance;
        private static object syncRoot = new object();

        private XmlOperator()
        {
        }

        public static XmlOperator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new XmlOperator();
                    }
                }
                return instance;
            }
        }

        public string GetNodeValue(string fileName,string xPath)
        {
            if (!File.Exists(fileName))
                return string.Empty;
            else
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);

                XmlNode xmlNode = xmlDocument.SelectSingleNode(xPath);
                if (xmlNode != null)
                    return xmlNode.InnerText;
                return string.Empty;
            }
        }

        public bool SetNodeValue(string fileName, string xPath,string value)
        {
            if (!File.Exists(fileName))
                return false;
            else
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);

                XmlNode xmlNode = xmlDocument.SelectSingleNode(xPath);
                if (xmlNode != null)
                {
                    xmlNode.InnerText = value;
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
