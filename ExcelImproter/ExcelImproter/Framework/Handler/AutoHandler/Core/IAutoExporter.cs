using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelImproter.Framework.Handler
{
    interface IAutoExporter
    {
        void DoExport(ConfigData configInfo);
        void Clear();
    }
}
