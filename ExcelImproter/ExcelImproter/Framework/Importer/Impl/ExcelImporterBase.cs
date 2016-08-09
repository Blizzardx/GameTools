using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelImproter.Framework.Reader;

namespace ExcelImproter.Framework.Importer.Impl
{
    public abstract class ExcelImporterBase: IImporter
    {
        public void Import(string path,out ImporterPkg outPkg)
        {
            outPkg = null;
            var reader = GetReader();

            ExcelData content = reader.Read(path + GetPath()) as ExcelData;
            string errMsg = string.Empty;

            AutoParasTable(content.DataList, ref errMsg);
            if (!CheckErrorMsg(errMsg))
            {
                return;
            }
            // custom importer
            ImporteExcel(content, out outPkg, ref errMsg);

            if (!CheckErrorMsg(errMsg))
            {
                return;
            }
            
        }
        public string GetPath()
        {
            return GetConfigPath();
        }
        protected virtual ExcelReaderBase GetReader()
        {
            return new ExcelReader_DB();
            //return new ExcelReader_ER();
        }

        #region auto paras
        protected virtual void AutoParasTable(List<ExcelTable> sheetValues, ref string errMsg)
        {
        }
        protected virtual void OnAutoParasBegin()
        {
        }
        protected virtual void OnAutoParasLine(int sheetIndex, int row, List<string> line, ref string errMsg)
        {

        }
        #endregion

        #region custom

        protected bool CheckErrorMsg(string errorMsg)
        {
            if (string.IsNullOrEmpty(errorMsg))
            {
                return true;
            }
            LogQueue.Instance.Enqueue(errorMsg);
            return false;
        }
        protected abstract void ImporteExcel(ExcelData data, out ImporterPkg outPkg, ref string errMsg);
        protected abstract string GetConfigPath();

        #endregion
    }
}
