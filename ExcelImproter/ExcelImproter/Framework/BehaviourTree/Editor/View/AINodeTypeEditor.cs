using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ExcelImproter.Framework.BehaviourTree.Editor.Controller;

namespace ExcelImproter.Framework.BehaviourTree.Editor.View
{
    public partial class AINodeTypeEditor : Form
    {
        private List<BTNodeTypeInfoData> m_TypeInfoList;
        private List<AINodeTypeEditorPanel> m_TypeEditPanelList;
        private List<AINodeTypeOptionEditorPanel> m_TypeOptionEditPanelList;
        private BTNodeTypeInfoData m_CurrentEditNode;

        public AINodeTypeEditor()
        {
            InitializeComponent();
            m_TypeInfoList = new List<BTNodeTypeInfoData>();
            m_TypeEditPanelList = new List<AINodeTypeEditorPanel>();
            m_TypeOptionEditPanelList = new List<AINodeTypeOptionEditorPanel>();
        }
        private void buttonLoad_Click(object sender, EventArgs e)
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
            Save();
            string errorMsg = string.Empty;
            if (!CheckData(ref errorMsg))
            {
                MessageBox.Show(this, errorMsg, "参数错误", MessageBoxButtons.OK);
                return;
            }
            BTNodeTypeManager.Instance.SaveTypeList(BTConfigSetting.BTNodeTypeConfigPath, m_TypeInfoList);
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
            bool haveRoot = false;
            for (int i = 0; i < m_TypeInfoList.Count; ++i)
            {
                var elem = m_TypeInfoList[i];
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
                    elem.m_OptionChildTypeList.Clear();
                }
                if (string.IsNullOrEmpty(elem.m_strName))
                {
                    errorMsg = "node type name can't be null ";
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
    }
}
