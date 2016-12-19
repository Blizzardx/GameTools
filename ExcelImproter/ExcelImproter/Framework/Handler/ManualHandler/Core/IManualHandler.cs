using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelImproter.Framework.Exporter;
using ExcelImproter.Framework.Importer;

namespace ExcelImproter.Framework.Handler
{
    public  interface IManualHandler
    {
        IImporter GetImporter();
        IExporter GetExporter();
    }
}
