using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Store.Core.Models
{
    public class EURrate : CbrfConnection
    {
        public decimal GetEUR()
        {
            decimal rateValue;
            XmlElement xRoot = GetXmlElement();
            XmlNode eurNode = xRoot.SelectSingleNode("Valute[@ID='R01239']/Value");
            return rateValue = decimal.Parse(eurNode.InnerText); // по хорошему tryparse, но xml центробанка не меняется, врядли будет ошибка
        }
    }
}