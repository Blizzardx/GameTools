using ExcelImproter.Framework.Reader;

namespace ExcelImproter.Project
{
    public abstract class ConfigHandlerBase 
    {
        protected string m_strConfigName;
           
        public static string GetConfigNameByClassName(string className)
        {
            return className.Split('_')[1];
        }
        public ConfigHandlerBase()
        {            
            m_strConfigName = GetConfigNameByClassName(GetType().Name);
        }
        public string GetConfigName()
        {
            return m_strConfigName;
        }
        abstract public string HandleConfig(ExcelData content);
    }
}
