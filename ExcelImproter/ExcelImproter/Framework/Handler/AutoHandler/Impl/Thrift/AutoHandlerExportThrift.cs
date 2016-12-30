using System;
using System.Collections.Generic;
using ExcelImproter.Configs;

namespace ExcelImproter.Framework.Handler
{
    class AutoHandlerExportThrift : IAutoExporter
    {
        private Packer_Thrift m_ThriftPacker;

        public void DoExport(ConfigData configInfo)
        {
            if (configInfo.GetConfigInfo().m_FileType == ConfigType.Excel)
            {
                ExportExcel(configInfo.GetExcelContent(),configInfo.GetConfigInfo().m_FilePath);
            }
            else
            {
                ExportTxt(configInfo.GetStrContent());
            }
        }
        public void Clear()
        {
        }
        private void ExportExcel(PackDataStruct content, string mFilePath)
        {
            m_ThriftPacker = new Packer_Thrift();
            var bytes = m_ThriftPacker.DoPack(content);
            FileUtils.WriteByteFile(mFilePath + ".byte", bytes);
        }
        public void ExportTxt(string content)
        {

        }
    }
}
