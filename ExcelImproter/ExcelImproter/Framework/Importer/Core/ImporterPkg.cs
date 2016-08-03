using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Config;
using Thrift.Protocol;

namespace ExcelImproter.Framework.Importer
{
    public class ImporterPkg
    {
        public TBase tBase;
        public string xmlContent;
        public XmlConfigBase xmlroot;
    }
}
