using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ExcelImproter.Framework.Reader.Core
{
    public interface IReader
    {
        IConfigContent Read(string filePath);
    }
}
