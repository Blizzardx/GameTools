using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ExcelImproter.Configs;

namespace ExcelImproter.Framework.Handler
{
    class ParserTxtDataToPackData
    {
        public string DoParser(string path)
        {
            return FileUtils.ReadStringFile(path);
        }
    }
}
