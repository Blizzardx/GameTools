using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ExcelImproter.Framework.Reader
{ 

    public class XmlReader_Common:XmlReaderBase
    {
        public override XmlData ReadXml(string path)
        {
            XmlData res = new XmlData();
            res.Root = XElement.Load(path);
            res.Content = File.ReadAllText(path);
            return res;
        }
    }
}
