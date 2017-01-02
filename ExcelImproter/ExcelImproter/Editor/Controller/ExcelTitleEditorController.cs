using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExcelImproter.Framework.Handler;

namespace ExcelImproter.Editor.Controller
{
    public class ExcelTitleViewNode : TreeNode
    {
        private ExcelDataElement m_Data;

        public void SetData(ExcelDataElement data)
        {
            m_Data = data;
        }
        public ExcelDataElement GetData()
        {
            return m_Data;
        }
    }
    public class ExcelTitleEditorController
    {
        public List<ExcelTitleViewNode> ConvertDataToView(ExcelDescInfoList data)
        {
            List<ExcelTitleViewNode> res = new List<ExcelTitleViewNode>();
            if (null == data || data.m_DescList == null || data.m_DescList.Count == 0)
            {
                return res;
            }
            for (int i = 0; i < data.m_DescList.Count; ++i)
            {
                res.Add(ConvertDataToView(data.m_DescList[i]));
            }
            return res;
        }
        private ExcelTitleViewNode ConvertDataToView(ExcelDataElement data)
        {
            ExcelTitleViewNode node = new ExcelTitleViewNode();
            node.SetData(data);
            node.Text = data.m_Type + ":" + data.m_strName;

            if (data is ExcelDataElement_List)
            {
                ExcelDataElement_List list = data as ExcelDataElement_List;
                node.Nodes.Add(ConvertDataToView(list.m_Value));
            }
            if (data is ExcelDataElement_Set)
            {
                ExcelDataElement_Set list = data as ExcelDataElement_Set;
                node.Nodes.Add(ConvertDataToView(list.m_Value));
            }
            if (data is ExcelDataElement_Map)
            {
                ExcelDataElement_Map list = data as ExcelDataElement_Map;
                node.Nodes.Add(ConvertDataToView(list.m_KeyValue));
                node.Nodes.Add(ConvertDataToView(list.m_ValueValue));
            }
            if (data is ExcelDataElement_Struct)
            {
                ExcelDataElement_Struct list = data as ExcelDataElement_Struct;
                for (int i = 0; i < list.m_Value.Count; ++i)
                {
                    node.Nodes.Add(ConvertDataToView(list.m_Value[i]));   
                }
            }
            return node;
        }
        public ExcelDescInfoList ConvertViewToData(List<TreeNode> data)
        {
            var res  = new ExcelDescInfoList();
            res.m_DescList = new List<ExcelDataElement_Struct>();
            for (int i = 0; i < data.Count; ++i)
            {
                ExcelTitleViewNode node = data[i] as ExcelTitleViewNode;
                res.m_DescList.Add(node.GetData() as ExcelDataElement_Struct);
            }
            Clean(res);
            return res;
        }
        public void Clean(ExcelDescInfoList data)
        {
            if (null == data || null == data.m_DescList || data.m_DescList.Count == 0)
            {
                return;
            }
            for (int i = 0; i < data.m_DescList.Count; ++i)
            {
                Clean(data.m_DescList[i]);
            }
        }
        private void Clean(ExcelDataElement data)
        {
            if (!data.m_bIsCheckIndex)
            {
                data.m_IndexConfigColumnId = 0;
                data.m_IndexConfigName = null;
            }
            if (!data.m_bIsLimitOption)
            {
                data.m_OptionList = null;
            }
            if (!data.m_bIsLimitRange)
            {
                data.m_RangeMax = null;
                data.m_RangeMin = null;
            }
            if (data is ExcelDataElement_List)
            {
                ExcelDataElement_List list = data as ExcelDataElement_List;
                Clean(list.m_Value);
            }
            if (data is ExcelDataElement_Set)
            {
                ExcelDataElement_Set list = data as ExcelDataElement_Set;
                Clean(list.m_Value);
            }
            if (data is ExcelDataElement_Map)
            {
                ExcelDataElement_Map list = data as ExcelDataElement_Map;
                Clean(list.m_KeyValue);
                Clean(list.m_ValueValue);
            }
            if (data is ExcelDataElement_Struct)
            {
                ExcelDataElement_Struct list = data as ExcelDataElement_Struct;
                for (int i = 0; i < list.m_Value.Count; ++i)
                {
                    Clean(list.m_Value[i]);
                }
            }
        }
    }
}
