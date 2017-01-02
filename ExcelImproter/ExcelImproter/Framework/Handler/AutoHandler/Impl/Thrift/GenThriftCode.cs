using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Tool;
using ExcelImproter.Configs;

namespace ExcelImproter.Framework.Handler
{
    class GenThriftCode : Singleton<GenThriftCode>
    {
        private List<StringBuilder> structInfoList;
        private HashSet<string> m_StructNameMap;
 
        public void GenCode(ExcelDescInfoList info)
        {
            if (null == info || info.m_DescList == null)
            {
                return;
            }
            structInfoList = new List<StringBuilder>();
            m_StructNameMap = new HashSet<string>();

            for (int i = 0; i < info.m_DescList.Count; ++i)
            {
                GenCode(info.m_DescList[i]);
            }
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < structInfoList.Count; ++i)
            {
                res.Append(structInfoList[i]);
            }
            FileUtils.WriteStringFile(HandlerConfigSetting.ThriftGenCodeOutputPath,res.ToString());
        }
        private string GenCode(ExcelDataElement data)
        {
            string elemType = string.Empty;

            if (data is ExcelDataElement_List)
            {
                ExcelDataElement_List list = data as ExcelDataElement_List;
                GenCode(list.m_Value);
                elemType = "list<{0}>";
                elemType = elemType.Replace("{0}", ConvertToThriftType(list.m_Value.m_Type, list.m_Value));
            }
            else if (data is ExcelDataElement_Set)
            {
                ExcelDataElement_Set list = data as ExcelDataElement_Set;
                GenCode(list.m_Value);
                elemType = "set<{0}>";
                elemType = elemType.Replace("{0}", ConvertToThriftType(list.m_Value.m_Type, list.m_Value));
            }
            else if (data is ExcelDataElement_Map)
            {
                ExcelDataElement_Map list = data as ExcelDataElement_Map;
                GenCode(list.m_KeyValue);
                GenCode(list.m_ValueValue);
                elemType = "map<{0},{1}>";
                elemType = elemType.Replace("{0}", ConvertToThriftType(list.m_KeyValue.m_Type, list.m_KeyValue));
                elemType = elemType.Replace("{1}", ConvertToThriftType(list.m_ValueValue.m_Type, list.m_ValueValue));
            }
            else if (data is ExcelDataElement_Struct)
            {
                ExcelDataElement_Struct list = data as ExcelDataElement_Struct;
                StringBuilder res = new StringBuilder();
                res.Append("struct ");
                res.Append(data.m_strName);
                res.Append('\n');
                res.Append('{');
                res.Append('\n');
                for (int i = 0; i < list.m_Value.Count; ++i)
                {
                    res.Append(GenCode(list.m_Value[i]));
                }
                res.Append('\n');
                res.Append('}');
                res.Append('\n');
                structInfoList.Add(res);
                elemType = list.m_strParamterName;
                if (m_StructNameMap.Contains(data.m_strName))
                {
                    // log error
                    throw new Exception("name already exist in struct");
                }
                else
                {
                    m_StructNameMap.Add(data.m_strName);
                }

            }
            else
            {
                elemType = ConvertToThriftType(data.m_Type, data);
            }
            StringBuilder elemRes = new StringBuilder();
            elemRes.Append(data.m_Id);
            elemRes.Append(": ");
            elemRes.Append(elemType);
            elemRes.Append(" ");
            elemRes.Append(data.m_strName);
            elemRes.Append('\n');
            return elemRes.ToString();
        }
        private string ConvertToThriftType(PackDataElementType mType,ExcelDataElement data )
        {
            switch (mType)
            {
                case PackDataElementType.Bool:
                    return "bool";
                    case PackDataElementType.Byte:
                    return "byte";
                    case PackDataElementType.Double:
                    return "double";
                    case PackDataElementType.I16:
                    return "i16";
                    case PackDataElementType.I32:
                    return "i32";
                    case PackDataElementType.I64:
                    return "i64";
                    case PackDataElementType.String:
                    return "string";
                    case PackDataElementType.Struct:
                    return data.m_strName;
            }
            return null;
        }
    }
}
