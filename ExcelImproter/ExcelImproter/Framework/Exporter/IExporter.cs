using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelImproter.Framework.Exporter
{
    public interface IExporter
    {
        void Export(object param);
    }
}
