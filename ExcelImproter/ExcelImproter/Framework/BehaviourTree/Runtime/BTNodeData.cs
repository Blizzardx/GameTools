using System.Collections.Generic;

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
        public BTNodeParamDataType m_Type;
        public string m_strName;
        public string m_Value;
    }
    public class BTNodeData
    {
        public string   m_strName;
        public string   m_strType;
        public string   m_strDesc;
        public int      m_Id;
        public List<BTNodeParamData>    m_ParamList;
        public List<BTNodeData>         m_ChildList;
    }
}
