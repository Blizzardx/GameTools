using System.Collections.Generic;
using Common.Config;
using Common.Tool;
using ExcelImproter.Configs;

namespace ExcelImproter.Framework.BehaviourTree.Editor.Controller
{
    public class BTNodeTypeData:XmlConfigBase
    {
        public List<string> m_OptionTypeList;
    }
    public class BTNodeTypeManager : Singleton<BTNodeTypeManager>
    {
        private List<string> m_OptionTypeList;

        public void LoadTypeList(string path)
        {
            string content = FileUtils.ReadStringFile(path);
            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            var data=XmlConfigBase.DeSerialize<BTNodeTypeData>(content);
            if (data == null || data.m_OptionTypeList == null)
            {
                return;
            }
            m_OptionTypeList = data.m_OptionTypeList;
        }
        public void SaveTypeList(string path,List<string> optionList )
        {
            m_OptionTypeList = optionList;
            BTNodeTypeData data = new BTNodeTypeData();
            data.m_OptionTypeList = m_OptionTypeList;
            string content = XmlConfigBase.Serialize(data);
            FileUtils.WriteStringFile(path, content);
        }
        public List<string> GetOptionTypeList()
        {
            return m_OptionTypeList;  
        } 
    }
}
