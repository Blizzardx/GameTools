using System;
using System.Collections.Generic;
using Common.Config;
using Common.Tool;
using ExcelImproter.Configs;

namespace ExcelImproter.Framework.Handler
{
    class ExcelDescManager:Singleton<ExcelDescManager>
    {
        private ExcelDescInfoList m_ExcelDescList;

        public ExcelDescInfo GetDescByName(string name)
        {
            if (null == m_ExcelDescList)
            {
                Load();
            }
            string configName = "";
            string tmp = "";
            HandlerCommon.FileDataInfo.HandleConfig(name, ref configName, ref tmp);
            int index = configName.IndexOf('.');
            configName = configName.Substring(0, index);
            configName += "Table";
            var tmpc = configName[0].ToString();
            configName = tmpc.ToUpper() + configName.Substring(1);
            for (int i = 0; i < m_ExcelDescList.m_DescList.Count; ++i)
            {
                if (m_ExcelDescList.m_DescList[i].m_ExcelTitleDesc.m_strName == configName)
                {
                    return m_ExcelDescList.m_DescList[i];
                }
            }
            return null;
        }
        private void Load()
        {
            var content = FileUtils.ReadStringFile(HandlerConfigSetting.ExcelDescConfigPath);
            m_ExcelDescList = XmlConfigBase.DeSerialize<ExcelDescInfoList>(content, getAllTypes().ToArray());
        }
        public void test()
        {
            /*
            struct SpellCardConfigTable
            {
	            1:map<i32,struct.SpellCardConfig> spellCardConfigMap
            }
            struct SpellCardConfig
            {
	            10: i32 id
	            20: i32 nameId
	            30: i32 descId
	            40: string icon
	            50: i32 activeLimitId
	            60: i32 activeCostId
	            70: i32 sortId
	            80: i32 displayLimitId
	            90: i32 tipType
	            100: i32 quality
            }
            */
            // test code

            #region test code
            ExcelDescInfoList list = new ExcelDescInfoList();
            list.m_DescList = new List<ExcelDescInfo>();
            ExcelDescInfo m_ExcelHeader = new ExcelDescInfo();
            list.m_DescList.Add(m_ExcelHeader);

            m_ExcelHeader.m_ExcelTitleDesc = new ExcelDataElement_Struct();
            m_ExcelHeader.m_ExcelTitleDesc.m_Id = -1;
            m_ExcelHeader.m_ExcelTitleDesc.m_strName = "SpellCardConfigTable";
            m_ExcelHeader.m_ExcelTitleDesc.m_Type = PackDataElementType.Struct;
            m_ExcelHeader.m_ExcelTitleDesc.m_iColumnId = -1;

            m_ExcelHeader.m_ExcelTitleDesc.m_Value = new List<ExcelDataElement>();
            ExcelDataElement_Map elem1 = new ExcelDataElement_Map();
            m_ExcelHeader.m_ExcelTitleDesc.m_Value.Add(elem1);

            elem1.m_strName = "spellCardConfigMap";
            elem1.m_Id = 1;
            elem1.m_Type = PackDataElementType.Map;
            elem1.m_iColumnId = -1;
            elem1.m_KeyValue = new ExcelDataElement();
            var value = new ExcelDataElement_Struct();
            elem1.m_ValueValue = value;

            elem1.m_KeyValue.m_strName = "none";
            elem1.m_KeyValue.m_Id = -1;
            elem1.m_KeyValue.m_Type = PackDataElementType.I32;
            elem1.m_KeyValue.m_iColumnId = 0;

            value.m_strName = "SpellCardConfig";
            value.m_Id = -1;
            value.m_Type = PackDataElementType.Struct;
            value.m_iColumnId = -1;
            value.m_Value = new List<ExcelDataElement>();
            ExcelDataElement subElem1 = new ExcelDataElement_Struct();
            ExcelDataElement subElem2 = new ExcelDataElement_Struct();
            ExcelDataElement subElem3 = new ExcelDataElement_Struct();
            ExcelDataElement subElem4 = new ExcelDataElement_Struct();
            ExcelDataElement subElem5 = new ExcelDataElement_Struct();
            ExcelDataElement subElem6 = new ExcelDataElement_Struct();
            ExcelDataElement subElem7 = new ExcelDataElement_Struct();
            ExcelDataElement subElem8 = new ExcelDataElement_Struct();
            ExcelDataElement subElem9 = new ExcelDataElement_Struct();
            ExcelDataElement subElem10 = new ExcelDataElement_Struct();
            value.m_Value.Add(subElem1);
            value.m_Value.Add(subElem2);
            value.m_Value.Add(subElem3);
            value.m_Value.Add(subElem4);
            value.m_Value.Add(subElem5);
            value.m_Value.Add(subElem6);
            value.m_Value.Add(subElem7);
            value.m_Value.Add(subElem8);
            value.m_Value.Add(subElem9);
            value.m_Value.Add(subElem10);
            #endregion

            subElem1.m_strName = "id";
            subElem1.m_Id = 10;
            subElem1.m_Type = PackDataElementType.I32;
            subElem1.m_iColumnId = 0;

            subElem2.m_strName = "nameId";
            subElem2.m_Id = 20;
            subElem2.m_Type = PackDataElementType.I32;
            subElem2.m_iColumnId = 3;

            subElem3.m_strName = "descId";
            subElem3.m_Id = 30;
            subElem3.m_Type = PackDataElementType.I32;
            subElem3.m_iColumnId = 5;

            subElem4.m_strName = "icon";
            subElem4.m_Id = 40;
            subElem4.m_Type = PackDataElementType.String;
            subElem4.m_iColumnId = 7;

            subElem5.m_strName = "activeLimitId";
            subElem5.m_Id = 50;
            subElem5.m_Type = PackDataElementType.I32;
            subElem5.m_iColumnId = 8;

            subElem6.m_strName = "activeCostId";
            subElem6.m_Id = 60;
            subElem6.m_Type = PackDataElementType.I32;
            subElem6.m_iColumnId = 9;

            subElem7.m_strName = "sortId";
            subElem7.m_Id = 70;
            subElem7.m_Type = PackDataElementType.I32;
            subElem7.m_iColumnId = 10;

            subElem8.m_strName = "displayLimitId";
            subElem8.m_Id = 80;
            subElem8.m_Type = PackDataElementType.I32;
            subElem8.m_iColumnId = 11;

            subElem9.m_strName = "tipType";
            subElem9.m_Id = 90;
            subElem9.m_Type = PackDataElementType.I32;
            subElem9.m_iColumnId = 12;

            subElem10.m_strName = "quality";
            subElem10.m_Id = 100;
            subElem10.m_Type = PackDataElementType.I32;
            subElem10.m_iColumnId = 1;

            
            var strContent = XmlConfigBase.Serialize(list, getAllTypes().ToArray());
            FileUtils.WriteStringFile(HandlerConfigSetting.ExcelDescConfigPath,strContent);
        }
        List<Type> getAllTypes()
        {
            List<Type> typelist = new List<Type>() {typeof(ExcelDataElement) };
            var list = ReflectionManager.Instance.GetTypeByBase(typeof (ExcelDataElement));
            if (null != list)
            {
                typelist.AddRange(list);
            }
            return typelist;
        }
    }
}
