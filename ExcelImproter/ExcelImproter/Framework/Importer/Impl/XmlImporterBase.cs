using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelImproter.Framework.Reader;

namespace ExcelImproter.Framework.Importer.Impl
{
    public abstract class XmlImporterBase : IImporter
    {
        public void Import(string path, out ImporterPkg outPkg)
        {
            var reader = GetReader();
            var content = reader.Read(path + GetPath()) as XmlData;
            string errorMsg = string.Empty;

            ImporterXml(content, out outPkg, ref errorMsg);

            if (!CheckErrorMsg(errorMsg))
            {
                return;
            }
        }

        public string GetPath()
        {
            return GetConfigPath();
        }
        protected virtual XmlReaderBase GetReader()
        {
            return new XmlReader_Common();
        }

        #region custom

        protected bool CheckErrorMsg(string errorMsg)
        {
            if (string.IsNullOrEmpty(errorMsg))
            {
                return true;
            }
            LogQueue.instance.Add(errorMsg);
            return false;
        }
        protected abstract void ImporterXml(XmlData data, out ImporterPkg outPkg, ref string errMsg);
        protected abstract string GetConfigPath();

        #endregion
    }
}
