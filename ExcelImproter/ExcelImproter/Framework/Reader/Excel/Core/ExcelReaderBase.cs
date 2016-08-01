using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelImproter.Framework.Reader.Core;

namespace ExcelImproter.Framework.Reader
{
    public abstract class ExcelReaderBase : IReader
    {
        abstract public ExcelData ReadExcel(string path);
        public IConfigContent Read(string filePath)
        {
            return ReadExcel(filePath);
        }
    }
}
