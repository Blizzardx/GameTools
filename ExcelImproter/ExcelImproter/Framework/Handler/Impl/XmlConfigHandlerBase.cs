using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelImproter.Framework.Exporter;
using ExcelImproter.Framework.Importer;
using ExcelImproter.Framework.Importer.Impl;

namespace ExcelImproter.Framework.Handler
{
    abstract class XmlConfigHandlerBase: XmlImporterBase,IExporter, IHandler
    {
        public void Export(object param)
        {
            DoImporter(param);
        }
        protected virtual void DoImporter(object param)
        {
            Export(param as ImporterPkg);
        }
        protected virtual void Export(ImporterPkg data)
        {

        }
        public IImporter GetImporter()
        {
            return this;
        }
        public IExporter GetExporter()
        {
            return this;
        }
    }
}
