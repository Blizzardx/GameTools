using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ExcelImproter.Framework.BehaviourTree.Editor.Controller;

namespace ExcelImproter.Framework.BehaviourTree.Editor
{
    public enum NodePanelOpr
    {
        Add,
        Update,
    }
    public partial class AINodeEditor : Form
    {
        private CustomViewNode m_Data;
        private CustomViewNode m_NewData;
        private NodePanelOpr m_CurrentOpr;

        public AINodeEditor()
        {
            InitializeComponent();
            Init();
        }
        private void buttonDone_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            if (Save(ref errMsg))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show(this, errMsg, "参数错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Refresh(TreeNode selectedNode, NodePanelOpr oper)
        {
            m_CurrentOpr = oper;
            m_Data = selectedNode as CustomViewNode;
            if (oper == NodePanelOpr.Add)
            {
                RefreshAdd();
            }
            else
            {
                RefrshUpdate();
            }
        }
        private void RefrshUpdate()
        {
            comboBoxNodeType.SelectedText = m_Data.GetData().m_strType;
            textBoxNodeDesc.Text = m_Data.GetData().m_strDesc;
            textBoxNodeNmae.Text = m_Data.GetData().m_strName;
            textBoxNodeId.Text = m_Data.GetData().m_Id.ToString();
        }
        private void RefreshAdd()
        {
            m_NewData = new CustomViewNode();
            BTNodeData data = new BTNodeData();
            m_NewData.SetData(data);
            comboBoxNodeType.SelectedIndex = 0;
        }
        private void Init()
        {
            BTNodeTypeManager.Instance.LoadTypeList("");
            var list = BTNodeTypeManager.Instance.GetOptionTypeList();
            list = new List<string>();
            // test code
            list.Add("Root");
            list.Add("Compose");
            list.Add("Action");
            list.Add("Compose");
            if (null != list)
            {
                for (int i = 0; i < list.Count; ++i)
                {
                    comboBoxNodeType.Items.Add(list[i]);
                }
            }

            for (int i = 0; i < 10; ++i)
            {
                var panel = new AINodeParamEditor();
                panel.Location = new Point(10,  i * panel.Size.Height);
                panel1.Controls.Add(panel);
            }
        }
        private bool Save(ref string errorMsg)
        {
            m_Data.GetData().m_strType = comboBoxNodeType.Items[comboBoxNodeType.SelectedIndex].ToString();
            m_Data.GetData().m_strDesc = textBoxNodeDesc.Text;
            m_Data.GetData().m_strName= textBoxNodeNmae.Text;
            var strId = textBoxNodeId.Text;
            if (!int.TryParse(strId, out m_Data.GetData().m_Id))
            {
                // show error tip
                errorMsg = "id is not i32";
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var panel = new AINodeParamEditor();
            panel.Location = new Point(10, 11 * panel.Size.Height);
            panel1.Controls.Add(panel);
        }
    }
}
