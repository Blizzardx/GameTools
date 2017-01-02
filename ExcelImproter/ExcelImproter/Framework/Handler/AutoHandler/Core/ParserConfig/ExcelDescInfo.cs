using System.Collections.Generic;
using System.Xml.Serialization;
using Common.Config;

namespace ExcelImproter.Framework.Handler
{
    public class ExcelDataElement
    {
        [XmlAttribute("ElementType")]
        public PackDataElementType m_Type;
        [XmlAttribute("Id")]
        public short m_Id;
        [XmlAttribute("Name")]
        public string m_strName;
        [XmlAttribute("ColumnId")]
        public int m_iColumnId;
        [XmlAttribute("isList")] 
        public bool m_bIsList;
        [XmlAttribute("isLimitRange")]
        public bool m_bIsLimitRange;
        [XmlAttribute("RangeMin")]
        public string m_RangeMin;
        [XmlAttribute("RangeMax")]
        public string m_RangeMax;
        [XmlAttribute("isLimitOption")] 
        public bool m_bIsLimitOption;
        public List<string> m_OptionList;
        [XmlAttribute("isCheckIndex")]
        public bool m_bIsCheckIndex;
        [XmlAttribute("IndexConfigName")]
        public string m_IndexConfigName;
        [XmlAttribute("IndexConfiggColumnId")] 
        public int m_IndexConfigColumnId;
    }
    public class ExcelDataElement_List: ExcelDataElement
    {
        public ExcelDataElement m_Value;
    }
    public class ExcelDataElement_Set : ExcelDataElement
    {
        public ExcelDataElement m_Value;
    }
    public class ExcelDataElement_Map : ExcelDataElement
    {
        public ExcelDataElement m_KeyValue;
        public ExcelDataElement m_ValueValue;
    }
    public class ExcelDataElement_Struct : ExcelDataElement
    {
        [XmlAttribute("ParamterName")]
        public string m_strParamterName;
        public List<ExcelDataElement> m_Value;
    }
    public class ExcelDescInfoList:XmlConfigBase
    {
        public List<ExcelDataElement_Struct> m_DescList;
    }
}
