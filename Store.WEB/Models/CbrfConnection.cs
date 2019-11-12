using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Store.WEB.Models
{
    public class CbrfConnection
    {
        public XmlElement GetXmlElement()
        {
            string url = "https://www.cbr-xml-daily.ru/daily_utf8.xml";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(url);
            XmlElement xRoot = xDoc.DocumentElement;
            return xRoot;
        }
    }
}