using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ExcelImproter.Framework.BehaviourTree.Editor.Controller;
using ExcelImproter.Framework.BehaviourTree.Editor.GenCode;
using GameConfigTools.Util;

namespace ExcelImproter.Framework.BehaviourTree.Editor.View
{
    public partial class AINodeTypeEditor : Form
    {
        private List<BTNodeTypeInfoData> m_TypeInfoList;
        private List<AINodeTypeEditorPanel> m_TypeEditPanelList;
        private List<AINodeTypeOptionEditorPanel> m_TypeOptionEditPanelList;
        private List<AINodeTypeParamterEditorPanel> m_TypeParmterEditPanelList;
        private BTNodeTypeInfoData m_CurrentEditNode;

        public AINodeTypeEditor()
        {
            InitializeComponent();
            m_TypeInfoList = new List<BTNodeTypeInfoData>();
            m_TypeEditPanelList = new List<AINodeTypeEditorPanel>();
            m_TypeOptionEditPanelList = new List<AINodeTypeOptionEditorPanel>();
            m_TypeParmterEditPanelList = new List<AINodeTypeParamterEditorPanel>();

            LoadAllData();
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadAllData();
        }
        private void LoadAllData()
        {
            BTNodeTypeManager.Instance.LoadTypeList(BTConfigSetting.BTNodeTypeConfigPath);
            m_TypeInfoList = BTNodeTypeManager.Instance.GetTypeInfoList();
            if (null == m_TypeInfoList)
            {
                m_TypeInfoList = new List<BTNodeTypeInfoData>();
            }
            RefreshTypeList();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveAllTypeData();
        }
        private bool SaveAllTypeData()
        {
            Save();
            string errorMsg = string.Empty;
            if (!CheckData(ref errorMsg) || !CheckAllParamterData(ref errorMsg))
            {
                MessageBox.Show(this, errorMsg, "参数错误", MessageBoxButtons.OK);
                return false;
            }
            BTNodeTypeManager.Instance.SaveTypeList(BTConfigSetting.BTNodeTypeConfigPath, m_TypeInfoList);
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // add
            OnAddType();
        }
        private void RefreshTypeList()
        {
            List<AINodeTypeEditorPanel> tmpPanelList = new List<AINodeTypeEditorPanel>();
            int i = 0;
            for (i = 0; i < m_TypeInfoList.Count; ++i)
            {
                AINodeTypeEditorPanel panel = null;
                if (i < m_TypeEditPanelList.Count)
                {
                    panel = m_TypeEditPanelList[i];
                }
                else
                {
                    panel = new AINodeTypeEditorPanel();
                    tmpPanelList.Add(panel);
                    panel.SetCallback(OnSubType, OnElemSelected);
                }
                panel.Visible = true;
                panel.Location = new Point(10, i * panel.Size.Height);
                panel1.Controls.Add(panel);
                panel.Refresh(m_TypeInfoList[i]);
            }
            if (i < m_TypeEditPanelList.Count)
            {
                for (; i < m_TypeEditPanelList.Count; ++i)
                {
                    m_TypeEditPanelList[i].Visible = false;
                }
            }
            m_TypeEditPanelList.AddRange(tmpPanelList);
        }
        private void RefreshOptionPanelList(BTNodeTypeInfoData rootData)
        {
            m_CurrentEditNode = rootData;
            List<AINodeTypeOptionEditorPanel> tmpPanelList = new List<AINodeTypeOptionEditorPanel>();
            int i = 0;
            for (i = 0; i < m_TypeInfoList.Count; ++i)
            {
                AINodeTypeOptionEditorPanel panel = null;
                if (i < m_TypeOptionEditPanelList.Count)
                {
                    panel = m_TypeOptionEditPanelList[i];
                }
                else
                {
                    panel = new AINodeTypeOptionEditorPanel();
                    panel.SetCallback(OnChangeOptionTypeList);
                    tmpPanelList.Add(panel);
                }
                panel.Visible = true;
                panel.Location = new Point(10, i * panel.Size.Height);
                panel2.Controls.Add(panel);
                panel.Refresh(rootData,m_TypeInfoList[i]);
            }
            if (i < m_TypeOptionEditPanelList.Count)
            {
                for (; i < m_TypeOptionEditPanelList.Count; ++i)
                {
                    m_TypeOptionEditPanelList[i].Visible = false;
                }
            }
            m_TypeOptionEditPanelList.AddRange(tmpPanelList);
        }
        private void Save()
        {
            foreach (var elem in m_TypeEditPanelList)
            {
                elem.Save();
            }
            //SaveOptionTypeList();
        }
        private void OnSubType(BTNodeTypeInfoData data)
        {
            Save();
            for (int i = 0; i < m_TypeInfoList.Count; ++i)
            {
                if (m_TypeInfoList[i] == data)
                {
                    m_TypeInfoList.RemoveAt(i);
                    break;
                }
            }
            RefreshTypeList();
        }
        private void OnAddType()
        {
            Save();
            BTNodeTypeInfoData elem = new BTNodeTypeInfoData();
            m_TypeInfoList.Add(elem);
            RefreshTypeList();
        }
        private void OnElemSelected(BTNodeTypeInfoData data)
        {
            Save();
            foreach (var elem in m_TypeEditPanelList)
            {
                if (elem.GetData() != data)
                {
                    elem.SetAsUnSelected();
                }
            }
            RefreshOptionPanelList(data);
            RefreshParamterPanleList(data);
        }
        private void OnChangeOptionTypeList(BTNodeTypeInfoData data, bool isSelected)
        {
            SaveOptionTypeList();
        }
        private void SaveOptionTypeList()
        {
            if (null == m_CurrentEditNode)
            {
                return;
            }
            List<string> optionList = new List<string>();
            foreach (var elem in m_TypeOptionEditPanelList)
            {
                if (elem.GetIsSelected())
                {
                    optionList.Add(elem.GetData().m_strName);
                }
            }
            if (optionList.Count > 0)
            {
                m_CurrentEditNode.m_OptionChildTypeList = optionList;
            }
        }
        private bool CheckData(ref string errorMsg)
        {
            HashSet<string> nameMap = new HashSet<string>();

            bool haveRoot = false;
            for (int i = 0; i < m_TypeInfoList.Count; ++i)
            {
                var elem = m_TypeInfoList[i];
                if (nameMap.Contains(elem.m_strName))
                {
                    errorMsg = "Already exist name with name : " + elem.m_strName;
                    return false;
                }
                nameMap.Add(elem.m_strName);

                if (elem.m_bIsRoot)
                {
                    if (haveRoot)
                    {
                        errorMsg = "Already exist root : " + elem.m_strName;
                        return false;
                    }
                    haveRoot = true;
                }
                if (elem.m_bIsLimitChildCount )
                {
                    if (elem.m_iLimitChildCount < 0)
                    {
                        errorMsg = "limit child count error : " + elem.m_strName;
                        return false;
                    }
                }
                else
                {
                    elem.m_iLimitChildCount = 0;
                }
                if (elem.m_bIsLimitChildType)
                {
                    if (elem.m_OptionChildTypeList.Count == 0)
                    {
                        errorMsg = "limit child type list is empty : " + elem.m_strName;
                        return false;
                    }
                }
                else
                {
                    if (null != elem.m_OptionChildTypeList)
                    {
                        elem.m_OptionChildTypeList.Clear();
                    }
                }
                if (string.IsNullOrEmpty(elem.m_strName))
                {
                    errorMsg = "node type name can't be null ";
                    return false;
                }
                if (!VaildUtil.IsChar(elem.m_strName))
                {
                    errorMsg = "type name can't be number";
                    return false;
                }
            }
            if (!haveRoot)
            {
                errorMsg = "Donot exist root in plan list";
                return false;
            }
            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // add paramter
            OnAddParamter();
        }
        private void RefreshParamterPanleList(BTNodeTypeInfoData rootData)
        {
            if (null == rootData.m_ParamList)
            {
                rootData.m_ParamList = new List<BTNodeTypeParamterData>();
            }
            List<AINodeTypeParamterEditorPanel> tmpPanelList = new List<AINodeTypeParamterEditorPanel>();
            int i = 0;
            for (i = 0; i < rootData.m_ParamList.Count; ++i)
            {
                AINodeTypeParamterEditorPanel panel = null;
                if (i < m_TypeParmterEditPanelList.Count)
                {
                    panel = m_TypeParmterEditPanelList[i];
                }
                else
                {
                    panel = new AINodeTypeParamterEditorPanel();
                    panel.SetCallback(OnSubParamter);
                    tmpPanelList.Add(panel);
                }
                panel.Visible = true;
                panel.Location = new Point(10, i * panel.Size.Height);
                panel3.Controls.Add(panel);
                panel.Refresh(rootData.m_ParamList[i]);
            }
            if (i < m_TypeParmterEditPanelList.Count)
            {
                for (; i < m_TypeParmterEditPanelList.Count; ++i)
                {
                    m_TypeParmterEditPanelList[i].Visible = false;
                }
            }
            m_TypeParmterEditPanelList.AddRange(tmpPanelList);
        }
        private void OnAddParamter()
        {
            if (null == m_CurrentEditNode)
            {
                return;
            }
            if (null == m_CurrentEditNode.m_ParamList)
            {
                m_CurrentEditNode.m_ParamList = new List<BTNodeTypeParamterData>();
            }
            BTNodeTypeParamterData elem = new BTNodeTypeParamterData();
            m_CurrentEditNode.m_ParamList.Add(elem);
            RefreshParamterPanleList(m_CurrentEditNode);
        }
        private void OnSubParamter(BTNodeTypeParamterData data)
        {
            if (null == m_CurrentEditNode)
            {
                return;
            }
            for (int i = 0; i < m_CurrentEditNode.m_ParamList.Count; ++i)
            {
                if (m_CurrentEditNode.m_ParamList[i] == data)
                {
                    m_CurrentEditNode.m_ParamList.RemoveAt(i);
                    break;
                }
            }
            RefreshParamterPanleList(m_CurrentEditNode);
        }
        private void buttonDone_Click(object sender, EventArgs e)
        {
            if (null == m_CurrentEditNode)
            {
                return;
            }
            for (int i = 0; i < m_TypeParmterEditPanelList.Count; ++i)
            {
                m_TypeParmterEditPanelList[i].Save();
            }
            string errorMsg = string.Empty;
            if (!CheckParamterData(ref errorMsg, m_CurrentEditNode))
            {
                MessageBox.Show(this, errorMsg, "参数错误", MessageBoxButtons.OK);
            }
        }
        private bool CheckAllParamterData(ref string errorMsg)
        {
            for (int i = 0; i < m_TypeInfoList.Count; ++i)
            {
                var elem = m_TypeInfoList[i];
                if (null == elem.m_ParamList)
                {
                    continue;
                }
                if (!CheckParamterData(ref errorMsg, elem))
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckParamterData(ref string errorMsg, BTNodeTypeInfoData elem)
        {
            HashSet<string> nameMap = new HashSet<string>();
            foreach (var info in elem.m_ParamList)
            {
                if (!CheckParamterData(ref errorMsg, info))
                {
                    errorMsg += " : in type: " + elem.m_strName;
                    return false;
                }
                if (nameMap.Contains(info.m_strName))
                {
                    errorMsg = string.Format("there are already exist node name {0} in {1}", info.m_strName, elem.m_strName);
                    return false;
                }
                if (info.m_strName == elem.m_strName)
                {
                    errorMsg = string.Format("paramter name can't same with type name {0} in {1}", info.m_strName, elem.m_strName);
                    return false;
                }
                nameMap.Add(info.m_strName);

            }
            return true;
        }
        private bool CheckParamterData(ref string errorMsg,BTNodeTypeParamterData data)
        {
            if (string.IsNullOrEmpty(data.m_strName))
            {
                errorMsg = "paramter name can't be null ";
                return false;
            }
            if (!VaildUtil.IsChar(data.m_strName))
            {
                errorMsg = "paramter name can't be number";
                return false;
            }
            return true;
        }
        private void buttonGenCode_Click(object sender, EventArgs e)
        {
            if (!SaveAllTypeData())
            {
                return;
            }
            GenCodeTool.Instance.GenCode(BTNodeTypeManager.Instance.GetTypeInfoList());
        }
    }
}
