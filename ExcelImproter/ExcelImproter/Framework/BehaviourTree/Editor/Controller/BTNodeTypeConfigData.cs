using System.Collections.Generic;
using System.Xml.Serialization;
using Common.Config;
using Common.Tool;
using ExcelImproter.Configs;

namespace ExcelImproter.Framework.BehaviourTree.Editor.Controller
{
    public class BTNodeTypeParamterData
    {
        [XmlAttribute("ParamType")]
        public BTNodeParamDataType m_Type;
        [XmlAttribute("Name")]
        public string m_strName;
    }
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

        public List<BTNodeTypeParamterData> m_ParamList;
    }
    public class BTNodeTypeConfigData:XmlConfigBase
    {
        public List<BTNodeTypeInfoData> m_TypeInfoList;
    }
    public class BTNodeTypeManager : Singleton<BTNodeTypeManager>
    {
        private List<BTNodeTypeInfoData> m_TypeInfoList;

        public void LoadTypeList(string path)
        {
            string content = FileUtils.ReadStringFile(path);
            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            var data=XmlConfigBase.DeSerialize<BTNodeTypeConfigData>(content);
            if (data == null || data.m_TypeInfoList == null)
            {
                return;
            }
            m_TypeInfoList   = data.m_TypeInfoList;
        }
        public void SaveTypeList(string path,List<BTNodeTypeInfoData> typeList )
        {
            m_TypeInfoList = typeList;
            BTNodeTypeConfigData data = new BTNodeTypeConfigData();
            data.m_TypeInfoList = m_TypeInfoList;
            string content = XmlConfigBase.Serialize(data);
            FileUtils.WriteStringFile(path, content);
        }
        public List<BTNodeTypeInfoData> GetTypeInfoList()
        {
            return m_TypeInfoList;
        }
    }
}
