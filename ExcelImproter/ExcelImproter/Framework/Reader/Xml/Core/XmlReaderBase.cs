using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelImproter.Framework.Reader.Core;

namespace ExcelImproter.Framework.Reader
{
    abstract public class XmlReaderBase : IReader
    {
        abstract public XmlData ReadXml(string path);
        public IConfigContent Read(string filePath)
        {
            return ReadXml(filePath);
        }
    }
}
