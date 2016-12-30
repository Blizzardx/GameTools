using ExcelImproter.Framework.Reader;

namespace ExcelImproter.Framework.Handler
{
    class AutoHandlerExportXml : IAutoExporter
    {
        public void DoExport(ConfigData configInfo)
        {
            if (configInfo.GetConfigInfo().m_FileType == ConfigType.Excel)
            {
                ExportExcel(configInfo.GetExcelContent());
            }
            else
            {
                ExportTxt(configInfo.GetStrContent());
            }
        }
        public void Clear()
        {
        }
        private void ExportExcel(PackDataStruct content)
        {
            
        }
        public void ExportTxt(string content)
        {
            
        }
    }
}
