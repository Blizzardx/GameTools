using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelImproter.Framework.Reader;
using GameConfigTools.Util;

namespace ExcelImproter.Framework.Handler
{
    class ParserExcelDataToPackData
    {
        private ExcelReaderBase m_Reader;
        private ExcelDataElement_Struct m_ExcelHeader;
        private PackDataStruct m_PackData;
        private List<string> m_CurrentLineData;
        private string m_ConfigPath;

        public PackDataStruct DoParser(string path)
        {
            m_ConfigPath = path;
            var content = GetReader().ReadExcel(path);
            LoadExcelDesc();
            return ParserExcelData(content);
        }
        private ExcelReaderBase GetReader()
        {
            if (null == m_Reader)
            {
                m_Reader = new ExcelReader_ER();
            }
            return m_Reader;
        }
        private void LoadExcelDesc()
        {
            m_ExcelHeader = ExcelDescManager.Instance.GetDescByName(m_ConfigPath);
        }
        private PackDataStruct ParserExcelData(ExcelData content)
        {
            if (null == content)
            {
                return null;
            }
            if (null == m_ExcelHeader)
            {
                return null;
            }
            List<PackDataElement> contentList = new List<PackDataElement>();

            foreach (var sheet in content.DataList)
            {
                int lineIndex = 0;
                foreach (var line in sheet.Data)
                {
                    m_CurrentLineData = line;
                    if (!IsNeedSkipLine())
                    {
                        contentList.Add(ParserLine(m_ExcelHeader));
                    }
                    ++ lineIndex;
                }
            }
            return MergeLine(contentList);
        }
        private PackDataElement ParserLine( ExcelDataElement data)
        {
            PackDataElement res = CreatePackDataByTitle(data);

            if (data.m_Type == PackDataElementType.Struct)
            {
                ExcelDataElement_Struct subData = data as ExcelDataElement_Struct;
                PackDataStruct packData = new PackDataStruct();
                packData.m_ElemList = new List<PackDataElement>();

                for (int i = 0; i < subData.m_Value.Count; ++i)
                {
                    packData.m_ElemList.Add(ParserLine(subData.m_Value[i]));
                }
                res.m_Value = packData;
            }
            else if (data.m_Type == PackDataElementType.List)
            {
                var packData = new List<PackDataElement>();
                ExcelDataElement_List subData = data as ExcelDataElement_List;
                packData.Add(ParserLine(subData.m_Value));
                res.m_Value = packData;
            }
            else if (data.m_Type == PackDataElementType.Set)
            {
                var packData = new HashSet<PackDataElement>();
                ExcelDataElement_Set subData = data as ExcelDataElement_Set;
                packData.Add(ParserLine(subData.m_Value));
                res.m_Value = packData;
            }
            else if (data.m_Type == PackDataElementType.Map)
            {
                var packData = new Dictionary<PackDataElement,PackDataElement>();
                ExcelDataElement_Map subData = data as ExcelDataElement_Map;
                var key = ParserLine( subData.m_KeyValue);
                var value = ParserLine( subData.m_ValueValue);
                packData.Add(key, value);
                res.m_Value = packData;
            }

            return res;
        }
        private PackDataElement CreatePackDataByTitle(ExcelDataElement data)
        {
            PackDataElement packdata = new PackDataElement();
            packdata.m_strName = data.m_strName;
            packdata.m_Id = data.m_Id;
            if (data.m_bIsList)
            {
                packdata.m_Type = PackDataElementType.List;
                packdata.m_Value = GetValueListByType(data.m_Type, data.m_iColumnId);   
            }
            else
            {
                packdata.m_Type = data.m_Type;
                packdata.m_Value = GetValueByType(data.m_Type, data.m_iColumnId);   
            }
            return packdata;
        }
        private object GetValueListByType(PackDataElementType type, int columeIndex)
        {
            if (columeIndex < 0 || columeIndex >= m_CurrentLineData.Count)
            {
                return null;
            }
            
            string s = m_CurrentLineData[columeIndex];
            switch (type)
            {
                case PackDataElementType.Bool:
                    return VaildUtil.SplitToList_bool(s);
                case PackDataElementType.Byte:
                    return VaildUtil.SplitToList_sbyte(s);
                case PackDataElementType.Double:
                    return VaildUtil.SplitToList_double(s);
                case PackDataElementType.I16:
                    return VaildUtil.SplitToList_short(s);
                case PackDataElementType.I32:
                    return VaildUtil.SplitToList_int(s);
                case PackDataElementType.I64:
                    return VaildUtil.SplitToList_long(s);
                case PackDataElementType.String:
                    return VaildUtil.SplitToList(s);
            }
            return null;
        }
        private object GetValueByType(PackDataElementType type, int columeIndex)
        {
            if (columeIndex < 0 || columeIndex >= m_CurrentLineData.Count)
            {
                return null;
            }
            string s = m_CurrentLineData[columeIndex];
            switch (type)
            {
                case PackDataElementType.Bool:
                {
                    bool value = false;
                    if (VaildUtil.TryConvert(s, out value))
                    {
                        return value;
                    }
                }
                    break;
                case PackDataElementType.Byte:
                    {
                        sbyte value;
                        if (VaildUtil.TryConvert(s, out value))
                        {
                            return value;
                        }
                    }
                    break;
                case PackDataElementType.Double:
                    {
                        double value ;
                        if (VaildUtil.TryConvert(s, out value))
                        {
                            return value;
                        }
                    }
                    break;
                case PackDataElementType.I16:
                    {
                        short value ;
                        if (VaildUtil.TryConvert(s, out value))
                        {
                            return value;
                        }
                    }
                    break;
                case PackDataElementType.I32:
                    {
                        int value ;
                        if (VaildUtil.TryConvert(s, out value))
                        {
                            return value;
                        }
                    }
                    break;
                case PackDataElementType.I64:
                    {
                        long value ;
                        if (VaildUtil.TryConvert(s, out value))
                        {
                            return value;
                        }
                    }
                    break;
                case PackDataElementType.String:
                    return s;
            }
            return null;
        }
        private PackDataStruct MergeLine(List<PackDataElement> contentList)
        {  
            if (contentList.Count < 1)
            {
                return null;
            }
            var baseElem = contentList[0];
            for (int i = 1; i < contentList.Count; ++i)
            {
                MergeLine(baseElem,contentList[i]);
            }
            return baseElem.m_Value as PackDataStruct;
        }
        private void MergeLine(PackDataElement baseElem, PackDataElement targetElem)
        {
            if (baseElem.m_Type == PackDataElementType.Struct)
            {
                PackDataStruct sourceData = baseElem.m_Value as PackDataStruct;
                PackDataStruct targetData = targetElem.m_Value as PackDataStruct;
                for (int i = 0; i < sourceData.m_ElemList.Count; ++i)
                {
                    MergeLine(sourceData.m_ElemList[i], targetData.m_ElemList[i]);
                }
            }
            else if (baseElem.m_Type == PackDataElementType.List)
            {
                List<PackDataElement> sourceData = baseElem.m_Value as List<PackDataElement>;
                List<PackDataElement> targetData = targetElem.m_Value as List<PackDataElement>;

                sourceData.AddRange(targetData);
            }
            else if (baseElem.m_Type == PackDataElementType.Set)
            {
                HashSet<PackDataElement> sourceData = baseElem.m_Value as HashSet<PackDataElement>;
                HashSet<PackDataElement> targetData = targetElem.m_Value as HashSet<PackDataElement>;
                foreach (var elem in targetData)
                {
                    sourceData.Add(elem);
                }
            }
            else if (baseElem.m_Type == PackDataElementType.Map)
            {
                var sourceData =baseElem.m_Value as Dictionary<PackDataElement, PackDataElement>;
                var targetData =targetElem.m_Value as Dictionary<PackDataElement, PackDataElement>;
                foreach (var elem in targetData)
                {
                    foreach (var elemSource in sourceData)
                    {
                        if (elemSource.Key.m_Value == elem.Key.m_Value)
                        {
                            // log error
                        }
                    }
                    sourceData.Add(elem.Key,elem.Value);
                }
                
            }
        }
        private bool IsNeedSkipLine()
        {
            if (null == m_CurrentLineData)
            {
                return true;
            }
            if (m_CurrentLineData.Count == 0)
            {
                return true;
            }
            if (m_CurrentLineData[0].StartsWith("#"))
            {
                return true;
            }
            if (m_CurrentLineData[0].StartsWith("//"))
            {
                return true;
            }
            bool needskip = true;
            for (int i = 0; i < m_CurrentLineData.Count; ++i)
            {
                if (!string.IsNullOrEmpty(m_CurrentLineData[i]))
                {
                    needskip = false;
                    break;
                }
            }
            return needskip;
        }
    }
}
