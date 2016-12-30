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
        public List<ExcelDataElement> m_Value;
    }
    public class ExcelDescInfo
    {
        public ExcelDataElement_Struct m_ExcelTitleDesc;
    }
    public class ExcelDescInfoList:XmlConfigBase
    {
        public List<ExcelDescInfo> m_DescList;
    }
}
