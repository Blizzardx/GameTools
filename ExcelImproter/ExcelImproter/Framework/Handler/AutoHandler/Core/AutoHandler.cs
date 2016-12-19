using System;
using System.Collections.Generic;

namespace ExcelImproter.Framework.Handler
{
    class AutoHandler
    {
        private List<IAutoExporter> m_ExporterPool;

        public AutoHandler()
        {
            var list = ReflectionManager.Instance.GetTypeByBase(typeof (IAutoExporter));
            m_ExporterPool = new List<IAutoExporter>(list.Count);
            for (int i = 0; i < list.Count; ++i)
            {
                var tmpExporter  = Activator.CreateInstance(list[i]) as IAutoExporter;
                m_ExporterPool.Add(tmpExporter);
            }
        }
        public void Clear()
        {
            
        }
        public void Handler(string configPath)
        {
            var headerInfo = GetConfigInfoByPath(configPath);
            var header = new ConfigData(headerInfo);
            header.DoParser();

            for (int i = 0; i < m_ExporterPool.Count; ++i)
            {
                var exporter = m_ExporterPool[i];
                exporter.Clear();
                exporter.DoExport(header);
            }
        }
        private ConfigDataInfo GetConfigInfoByPath(string configPath)
        {
            ConfigDataInfo elem = new ConfigDataInfo();
            if (configPath.EndsWith(".xlsx") || configPath.EndsWith(".xls"))
            {
                elem.m_FileType = ConfigType.Excel;
            }
            else
            {
                elem.m_FileType = ConfigType.Txt;
            }
            elem.m_FilePath = configPath;
            return elem;
        }
    }
}
