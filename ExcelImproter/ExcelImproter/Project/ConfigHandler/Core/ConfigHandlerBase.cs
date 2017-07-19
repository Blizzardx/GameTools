using System.IO;
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

        protected void Output(byte[] content)
        {
            string path = SystemConst.Config.OutputPath + "/" + m_strConfigName + ".bytes";
            File.WriteAllBytes(path, content);
        }
        abstract public string HandleConfig(ExcelData content);
        abstract public bool CheckRefrenceConfig(ExcelData content, int id, string keyValue);
    }
}
