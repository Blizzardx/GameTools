using System;
using System.Windows.Forms;
using ExcelImproter.Framework.BehaviourTree.Editor.Controller;

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

        #region common

        public AINodeEditorPanel()
        {
            InitializeComponent();
            comboBoxNodeType.Items.AddRange(BTNodeTypeManager.Instance.GetOptionTypeList().ToArray());
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
        #endregion

        #region update node
        public void EditNode(CustomViewNode data)
        {
            m_Status = NodePanelOpr.Update;
            buttonDone.Visible = true;
            buttonAdd.Visible = false;
            buttonCancel.Visible = false;

            m_Data = data;
            RefrshUpdate();
        }
        private bool OnEndEdit(ref string errorMsg)
        {
            if (!CheckData(ref errorMsg))
            {
                return false;
            }
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
        public void CreateNode(Action<CustomViewNode> onCreateCallback)
        {
            m_Status = NodePanelOpr.Add;
            buttonDone.Visible = false;
            buttonAdd.Visible = true;
            buttonCancel.Visible = true;
            m_OnCreateCallback = onCreateCallback;
            m_Data = CreateDefaultNode();
            RefrshUpdate();
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
