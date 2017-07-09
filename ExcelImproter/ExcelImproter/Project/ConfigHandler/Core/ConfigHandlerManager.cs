using System;
using Common.Tool;
using System.Collections.Generic;
using ExcelImproter.Framework.Reader;

namespace ExcelImproter.Project
{
    public class ConfigHandlerManager : Singleton<ConfigHandlerManager>
    {
        private string m_strRootPath;
        private Dictionary<string, Type> m_HandlerMpa;
        private List<string> m_AllHandlerConfigNameList;
        private IExcelReader m_ExcelReader;

        public ConfigHandlerManager()
        {
            var allHandlerTypes = ReflectionManager.Instance.GetTypeByBase(typeof(ConfigHandlerBase));
            m_HandlerMpa = new Dictionary<string, Type>();
            m_AllHandlerConfigNameList = new List<string>(allHandlerTypes.Count);

            for (int i=0;i<allHandlerTypes.Count;++i)
            {
                var type = allHandlerTypes[i];
                var name = ConfigHandlerBase.GetConfigNameByClassName(type.Name);

                m_AllHandlerConfigNameList.Add(name);
                m_HandlerMpa.Add(name, type);
            }

            m_ExcelReader = new ExcelReader_ER();
            
        }
        public void SetConfigFolderPath(string folderPath)
        {
            m_strRootPath = folderPath;
        }
        public List<string> GetAllVaildConfigHandlerList()
        {
            return m_AllHandlerConfigNameList;
        }
        public string HandleConfig(string configName)
        {
            Type handlerType = null;
            if(! m_HandlerMpa.TryGetValue(configName, out handlerType))
            {
                return "cna't find config handler by name " + configName;
            }

            ConfigHandlerBase handler =  Activator.CreateInstance(handlerType) as ConfigHandlerBase;
            var realconfigName = m_strRootPath + configName;
            var content = m_ExcelReader.ReadExcel(realconfigName);
            var errorInfo = handler.HandleConfig(content);

            return errorInfo;
        }
    }
}
