using System;
using Common.Tool;
using System.Collections.Generic;
using ExcelImproter.Framework.Reader;
using ExcelImproter.Project.DynamicCompile;

namespace ExcelImproter.Project
{
    public class ConfigHandlerManager : Singleton<ConfigHandlerManager>
    {
        private Dictionary<string, Type> m_HandlerMpa;
        private List<string> m_AllHandlerConfigNameList;
        private IExcelReader m_ExcelReader;
        private Dictionary<string, ExcelData> m_DataCashe;
         
        public ConfigHandlerManager()
        {
            m_ExcelReader = new ExcelReader_ER();
            
            m_HandlerMpa = new Dictionary<string, Type>();
            m_AllHandlerConfigNameList = new List<string>();
            RefreshAllVaildConfigHandlerList();
        }
        public List<string> RefreshAllVaildConfigHandlerList()
        {
            var allHandlerTypes = ReflectionManager.Instance.GetTypeByBase(typeof(ConfigHandlerBase));
            allHandlerTypes.AddRange(DynamicCompiler.Instance.GetTypeByBase(typeof(ConfigHandlerBase)));

            m_HandlerMpa.Clear();
            m_AllHandlerConfigNameList.Clear();

            for (int i = 0; i < allHandlerTypes.Count; ++i)
            {
                var type = allHandlerTypes[i];
                var name = ConfigHandlerBase.GetConfigNameByClassName(type.Name);

                m_AllHandlerConfigNameList.Add(name);
                m_HandlerMpa.Add(name, type);
            }

            return m_AllHandlerConfigNameList;
        }
        public string HandleConfig(string configName)
        {
            try
            {
                LogQueue.Instance.Enqueue("Begein import : " + configName);

                m_DataCashe = new Dictionary<string, ExcelData>();

                Type handlerType = null;
                if (!m_HandlerMpa.TryGetValue(configName, out handlerType))
                {
                    return "can't find config handler by name " + configName;
                }

                ConfigHandlerBase handler = Activator.CreateInstance(handlerType) as ConfigHandlerBase;
                var realconfigName = SystemConst.Config.ExcelConfigPath + "/" + configName + ".xlsx";
                var content = LoadExcelFromCasheOrDisk(realconfigName);
                var errorInfo = handler.HandleConfig(content);

                return errorInfo;
            }
            catch(Exception e)
            {
                return e.Message + e.StackTrace;
            }
        }
        public bool CheckRefrenceConfig(string configName, int id,string keyValue)
        {
            if (string.IsNullOrEmpty(configName))
            {
                return true;
            }
            try
            {
                Type handlerType = null;
                if (!m_HandlerMpa.TryGetValue(configName, out handlerType))
                {
                    LogQueue.Instance.Enqueue("can't find config handler by name " + configName);
                    return false;
                }

                ConfigHandlerBase handler = Activator.CreateInstance(handlerType) as ConfigHandlerBase;
                var realconfigName = SystemConst.Config.ExcelConfigPath + "/" + configName + ".xlsx";
                var content = LoadExcelFromCasheOrDisk(realconfigName);

                return handler.CheckRefrenceConfig(content, id, keyValue);
            }
            catch (Exception e)
            {
                LogQueue.Instance.Enqueue(e.Message + e.StackTrace);
                return false;
            }
            return true;
        }
        private ExcelData LoadExcelFromCasheOrDisk(string configName)
        {
            ExcelData res = null;
            if (m_DataCashe.TryGetValue(configName, out res))
            {
                return res;
            }
            res = m_ExcelReader.ReadExcel(configName);
            m_DataCashe.Add(configName, res);
            return res;
        }
    }
}
