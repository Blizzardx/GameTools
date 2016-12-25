using System.Collections.Generic;
using System.Xml.Serialization;
using Common.Config;
using Common.Tool;
using ExcelImproter.Configs;

namespace ExcelImproter.Framework.BehaviourTree.Editor.Controller
{
    public class BTNodeTypeInfoData
    {
        [XmlAttribute("IsRoot")]
        public bool m_bIsRoot;

        [XmlAttribute("Name")]
        public string m_strName;

        [XmlAttribute("IsLimitChildCount")]
        public bool m_bIsLimitChildCount;

        [XmlAttribute("LimitChildCount")]
        public int m_iLimitChildCount;

        [XmlAttribute("IsLimitChildType")]
        public bool m_bIsLimitChildType;

        public List<string> m_OptionChildTypeList;
    }
    public class BTNodeTypeConfigData:XmlConfigBase
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
            var data=XmlConfigBase.DeSerialize<BTNodeTypeConfigData>(content);
            if (data == null || data.m_OptionTypeList == null)
            {
                return;
            }
            m_OptionTypeList = data.m_OptionTypeList;
        }
        public void SaveTypeList(string path,List<string> optionList )
        {
            m_OptionTypeList = optionList;
            BTNodeTypeConfigData data = new BTNodeTypeConfigData();
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
