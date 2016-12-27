using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ExcelImproter.Framework.BehaviourTree.Editor.Controller;
using GameConfigTools.Util;

namespace ExcelImproter.Framework.BehaviourTree.Editor.View
{
    public enum NodePanelOpr
    {
        Idle,
        Add,
        Update,
    }

    public partial class AINodeEditorPanel : UserControl
    {
        private CustomViewNode m_Data;
        private Action<CustomViewNode> m_OnCreateCallback;
        private NodePanelOpr m_Status;
        private List<AINodeParamEditor> m_ParamEditPanelList;

        #region common

        public AINodeEditorPanel()
        {
            InitializeComponent();
            m_ParamEditPanelList = new List<AINodeParamEditor>();
            
            if (null != BTNodeTypeManager.Instance.GetTypeInfoList())
            {
                var list = BTNodeTypeManager.Instance.GetTypeInfoList();
                List<string> typeList = new List<string>(list.Count);
                for (int i = 0; i < list.Count; ++i)
                {
                    typeList.Add(list[i].m_strName);
                }
                comboBoxNodeType.Items.AddRange(typeList.ToArray());
            }
        }
        public NodePanelOpr GetStatus()
        {
            return m_Status;
        }
        private void RefrshUpdate()
        {
            int index = 0;
            foreach (var elem in comboBoxNodeType.Items)
            {
                if (elem.ToString() == m_Data.GetData().m_strType)
                {
                    comboBoxNodeType.SelectedIndex = index;
                    break;
                }
                ++ index;
            }
            textBoxNodeDesc.Text = m_Data.GetData().m_strDesc;
            textBoxNodeNmae.Text = m_Data.GetData().m_strName;
            textBoxNodeId.Text = m_Data.GetData().m_Id.ToString();
            RefreshParamterEditList();
        }
        private bool CheckData(ref string errorMsg)
        {
            if (comboBoxNodeType.SelectedIndex == -1)
            {
                errorMsg = "undefined node type";
                return false;
            }
            m_Data.GetData().m_strType = comboBoxNodeType.Items[comboBoxNodeType.SelectedIndex].ToString();
            m_Data.GetData().m_strDesc = textBoxNodeDesc.Text;
            m_Data.GetData().m_strName = textBoxNodeNmae.Text;
            var strId = textBoxNodeId.Text;
            if (!int.TryParse(strId, out m_Data.GetData().m_Id))
            {
                // show error tip
                errorMsg = "id is not i32";
                return false;
            }
            return true;
        }
        private void RefreshParamterEditList()
        {
            if (null == m_Data.GetData().m_ParamList)
            {
                m_Data.GetData().m_ParamList = new List<BTNodeParamData>();
            }
            List<AINodeParamEditor> tmpList = new List<AINodeParamEditor>();
            int i = 0;
            for (i = 0; i < m_Data.GetData().m_ParamList.Count;++i)
            {
                AINodeParamEditor panel = null;
                if (i < m_ParamEditPanelList.Count)
                {
                    panel = m_ParamEditPanelList[i];
                }
                else
                {
                    panel = new AINodeParamEditor();
                    panel.SetCallback(OnSubOper);
                    tmpList.Add(panel);
                }
                panel.Visible = true;
                panel.Location = new Point(10, i * panel.Size.Height);
                panel1.Controls.Add(panel);
                panel.Refresh(m_Data.GetData().m_ParamList[i]);
            }
            if (i < m_ParamEditPanelList.Count)
            {
                for (; i < m_ParamEditPanelList.Count; ++i)
                {
                    panel1.Controls.Remove(m_ParamEditPanelList[i]);
                    m_ParamEditPanelList[i].Visible = false;
                }
            }
            m_ParamEditPanelList.AddRange(tmpList);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OnAddOper();
        }
        private void OnAddOper()
        {
            if (null == m_Data.GetData().m_ParamList)
            {
                m_Data.GetData().m_ParamList = new List<BTNodeParamData>();
            }
            BTNodeParamData paramData = new BTNodeParamData();
            m_Data.GetData().m_ParamList.Add(paramData);
            SaveEditData();
            RefreshParamterEditList();
        }
        private void OnSubOper(BTNodeParamData node)
        {
            if (null == m_Data.GetData().m_ParamList || m_Data.GetData().m_ParamList.Count == 0)
            {
                return;
            }
            for (int i = 0; i < m_Data.GetData().m_ParamList.Count; ++i)
            {
                if (node == m_Data.GetData().m_ParamList[i])
                {
                    m_Data.GetData().m_ParamList.RemoveAt(i);
                    break;
                }
            }
            SaveEditData();
            RefreshParamterEditList();
        }
        private void SaveEditData()
        {
            foreach (var elem in m_ParamEditPanelList)
            {
                elem.Save();
            }
        }
        private bool SaveParamList(ref string errorMsg)
        {
            SaveEditData();
            if (null == m_Data.GetData().m_ParamList)
            {
                return true;
            }
            for (int i = 0; i < m_Data.GetData().m_ParamList.Count; ++i)
            {
                var elem = m_Data.GetData().m_ParamList[i];
                if (!CheckParam(elem,ref errorMsg))
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckParam(BTNodeParamData data,ref string errorMsg)
        {
            if (string.IsNullOrEmpty(data.m_strName))
            {
                errorMsg = "node paramter name can't be null";
                return false;
            }
            switch (data.m_Type)
            {
                case BTNodeParamDataType.Bool:
                    if (data.m_Value != "0" && data.m_Value != "1")
                    {
                        errorMsg = "bool type with wrong value {0,1}";
                        return false;
                    }
                    break;
                case BTNodeParamDataType.Byte:
                    if (!VaildUtil.IsFormateCorrect_Byte(data.m_Value))
                    {
                        errorMsg = "Byte type with wrong value";
                        return false;
                    }
                    break;
                case BTNodeParamDataType.Double:
                    if (!VaildUtil.IsFormateCorrect_Double(data.m_Value))
                    {
                        errorMsg = "Double type with wrong value";
                        return false;
                    }
                    break;
                case BTNodeParamDataType.I16:
                    if (!VaildUtil.IsFormateCorrect_Short(data.m_Value))
                    {
                        errorMsg = "I16 type with wrong value";
                        return false;
                    }
                    break;
                case BTNodeParamDataType.I32:
                    if (!VaildUtil.IsFormateCorrect_Int(data.m_Value))
                    {
                        errorMsg = "I32 type with wrong value";
                        return false;
                    }
                    break;
                case BTNodeParamDataType.I64:
                    if (!VaildUtil.IsFormateCorrect_Long(data.m_Value))
                    {
                        errorMsg = "I64 type with wrong value";
                        return false;
                    }
                    break;
                case BTNodeParamDataType.String:
                    break;
            }
            return true;
        }
        #endregion

        #region update node
        public void EditNode(CustomViewNode data)
        {
            m_Status = NodePanelOpr.Update;
            buttonDone.Visible = true;
            buttonAdd.Visible = false;
            buttonCancel.Visible = false;
            comboBoxNodeType.Enabled = false;
            
            m_Data = data;

            RefrshUpdate();
        }
        private bool OnEndEdit(ref string errorMsg)
        {
            if (!CheckData(ref errorMsg))
            {
                return false;
            }
            if (!SaveParamList(ref errorMsg))
            {
                return false;
            }
            m_Data.Text = m_Data.GetData().m_strType + ":" + m_Data.GetData().m_strName;
            m_Status = NodePanelOpr.Idle;
            return true;
        }
        private void buttonDone_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty; 
            if (!OnEndEdit(ref errMsg))
            {
                MessageBox.Show(this, errMsg, "参数错误", MessageBoxButtons.OK);
            }
        }
        #endregion

        #region create node
        public void CreateNode(bool isRootNode,Action<CustomViewNode> onCreateCallback)
        {
            m_Status = NodePanelOpr.Add;
            buttonDone.Visible = false;
            buttonAdd.Visible = true;
            buttonCancel.Visible = true;
            comboBoxNodeType.Enabled = true;
            m_OnCreateCallback = onCreateCallback;
            m_Data = CreateDefaultNode();
            if (isRootNode)
            {
                foreach (var elem in BTNodeTypeManager.Instance.GetTypeInfoList())
                {
                    if (elem.m_bIsRoot)
                    {
                        comboBoxNodeType.SelectedItem = elem.m_strName;
                        break;
                    }
                }
                comboBoxNodeType.Enabled = false;
            }
            ReloadParamaterList();
            RefrshUpdate();
        }
        private void comboBoxNodeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Status == NodePanelOpr.Add)
            {
                ReloadParamaterList();
                RefrshUpdate();
            }
        }
        private void ReloadParamaterList()
        {
            if (comboBoxNodeType.Items.Count == 0)
            {
                return;
            }
            if (null == m_Data.GetData().m_ParamList)
            {
                m_Data.GetData().m_ParamList = new List<BTNodeParamData>();
            }
            else
            {
                m_Data.GetData().m_ParamList.Clear();
            }
            var list = BTNodeTypeManager.Instance.GetTypeInfoList();
            string curSelected = string.Empty;
            if (comboBoxNodeType.SelectedIndex != -1)
            {
                curSelected = comboBoxNodeType.Items[comboBoxNodeType.SelectedIndex] as string;
            }
            else
            {
                curSelected = comboBoxNodeType.Items[0] as string;
            }
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i].m_strName == curSelected)
                {
                    m_Data.GetData().m_strType = curSelected;
                    if (null != list[i].m_ParamList)
                    {
                        foreach (var elem in list[i].m_ParamList)
                        {
                            BTNodeParamData newElem = new BTNodeParamData();
                            newElem.m_strName = elem.m_strName;
                            newElem.m_Type = elem.m_Type;
                            m_Data.GetData().m_ParamList.Add(newElem);
                        }
                    }
                    break;
                }
            }
        }
        private CustomViewNode CreateDefaultNode()
        {
            CustomViewNode node = new CustomViewNode();
            BTNodeData data = new BTNodeData();
            node.SetData(data);
            return node;
        }
        private bool OnEndCreate(ref string errorMsg)
        {
            if (!CheckData(ref errorMsg))
            {
                return false;
            }
            if (!SaveParamList(ref errorMsg))
            {
                return false;
            }
            m_Status = NodePanelOpr.Idle;
            return true;
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            m_Status = NodePanelOpr.Idle;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            if (!OnEndCreate(ref errMsg))
            {
                MessageBox.Show(this, errMsg, "参数错误", MessageBoxButtons.OK);
            }
            else
            {
                m_OnCreateCallback(m_Data);
            }
        }
        #endregion

    }
}
