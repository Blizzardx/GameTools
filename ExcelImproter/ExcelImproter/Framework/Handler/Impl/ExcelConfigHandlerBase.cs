using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelImproter.Framework.Exporter;
using ExcelImproter.Framework.Importer;
using ExcelImproter.Framework.Importer.Impl;

namespace ExcelImproter.Framework.Handler
{
    public abstract class ExcelConfigHandlerBase : ExcelImporterBase, IExporter, IHandler
    {
        #region public interface
        public void Export(object param)
        {
            DoExport(param);
        }
        public IImporter GetImporter()
        {
            return SetImporter();
        }
        public IExporter GetExporter()
        {
            return SetExporter();
        }
        #endregion

        #region protected function
        protected virtual void DoExport(object param)
        {
            Export(param as ImporterPkg);
        }
        protected virtual void Export(ImporterPkg data)
        {

        }
        protected virtual IImporter SetImporter()
        {
            return this;
        }
        protected virtual IExporter SetExporter()
        {
            return this;
        }
        #endregion
    }
}
