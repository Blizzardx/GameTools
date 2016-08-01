using System.Xml.Linq;
using ExcelImproter.Framework.Reader.Core;

namespace ExcelImproter.Framework.Reader
{
    public class XmlData : IConfigContent
    {
        public string Content;
        public XElement Root;
    }
}
