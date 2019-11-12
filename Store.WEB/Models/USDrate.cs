using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Store.WEB.Models
{
    public class USDrate : CbrfConnection
    {
        public decimal GetUSD()
        {
            decimal rateValue;
            XmlElement xRoot = GetXmlElement();
            XmlNode usdNode = xRoot.SelectSingleNode("Valute[@ID='R01235']/Value");
            return rateValue = decimal.Parse(usdNode.InnerText); // по хорошему tryparse, но xml центробанка не меняется, врядли будет ошибка

        }
    }
}