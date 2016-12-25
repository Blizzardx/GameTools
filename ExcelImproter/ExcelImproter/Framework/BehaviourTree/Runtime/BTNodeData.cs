using System.Collections.Generic;
using System.Xml.Serialization;

namespace ExcelImproter.Framework.BehaviourTree
{
    public enum BTNodeParamDataType
    {
        Bool,
        Byte,
        Double,
        I16,
        I32,
        I64,
        String,
    }
    public static class BTNodeParamDataTypeDesc
    {
        static public BTNodeParamDataType[] BTNodeParamDataTypes = new BTNodeParamDataType[]
        {
            BTNodeParamDataType.Bool,
            BTNodeParamDataType.Byte,
            BTNodeParamDataType.Double,
            BTNodeParamDataType.I16,
            BTNodeParamDataType.I32,
            BTNodeParamDataType.I64,
            BTNodeParamDataType.String,
        };
    }
    public class BTNodeParamData
    {
        [XmlAttribute("ParamType")]
        public BTNodeParamDataType m_Type;
        [XmlAttribute("Name")]
        public string m_strName;
        [XmlAttribute("Value")]
        public string m_Value;
    }
    public class BTNodeData
    {
        [XmlAttribute("Name")]
        public string m_strName;
        [XmlAttribute("Type")]
        public string m_strType;
        [XmlAttribute("Desc")]
        public string m_strDesc;
        [XmlAttribute("Id")]
        public int      m_Id;
        public List<BTNodeParamData>    m_ParamList;
        public List<BTNodeData>         m_ChildList;
    }
}
