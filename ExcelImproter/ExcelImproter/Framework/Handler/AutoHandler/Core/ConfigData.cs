using ExcelImproter.Framework.Reader;

namespace ExcelImproter.Framework.Handler
{
    public enum ConfigType
    {
        Excel,
        Txt,
    }
    class ConfigDataInfo
    {
        public string m_FilePath;
        public ConfigType m_FileType;
    }
    class ConfigData
    {
        private ConfigDataInfo   m_Info;
        private string           m_StrContent;
        private ExcelData        m_ExcelContent;

        public ConfigData(ConfigDataInfo info)
        {
            m_Info = info;
        }
        public void DoParser()
        {
            if (m_Info.m_FileType == ConfigType.Excel)
            {
                m_ExcelContent = new ParserExcelDataToPackData().DoParser(m_Info.m_FilePath);
            }
            else
            {
                m_StrContent = new ParserTxtDataToPackData().DoParser(m_Info.m_FilePath);
            }
        }
        public string GetStrContent()
        {
            return m_StrContent;
        }
        public ExcelData GetExcelContent()
        {
            return m_ExcelContent;
        }
        public ConfigDataInfo GetConfigInfo()
        {
            return m_Info;
        }
    }
}
