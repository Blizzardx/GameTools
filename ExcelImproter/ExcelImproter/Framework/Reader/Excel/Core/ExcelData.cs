using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelImproter.Framework.Reader.Core;

namespace ExcelImproter.Framework.Reader
{
    public class ExcelData : IConfigContent
    {
        public List<ExcelTable> DataList;
    }

    public class ExcelTable
    {
        public List<List<string>> Data;
    }
}
