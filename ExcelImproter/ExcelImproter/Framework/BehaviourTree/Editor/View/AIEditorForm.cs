using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ExcelImproter.Framework.BehaviourTree.Editor.Controller;
using ExcelImproter.Framework.BehaviourTree.Editor.View;

namespace ExcelImproter.Framework.BehaviourTree.Editor
{
    public partial class AIEditorForm : Form
    {
        private AIEditorController m_Controller;
        private AINodeEditorPanel m_NodeEditorPanel;
        private string m_strCurrentEditFilePath;

        public AIEditorForm()
        {
            InitializeComponent();
            treeView.AfterSelect += OnClickTreeItem;
            m_Controller = new AIEditorController();
            BTNodeTypeManager.Instance.LoadTypeList(BTConfigSetting.BTNodeTypeConfigPath);
            InitPanel();
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            //BTNodeParser.Instance.Save("e:\\temp.txt");
            BTNodeParser.Instance.LoadBTPlan("e:\\tmp.txt");
            List<CustomViewNode> viewNodeList = m_Controller.ConvertDateNodeListToViewNodeList(BTNodeParser.Instance.GetPlanList());
            if (null == viewNodeList)
            {
                return;
            }
            treeView.Nodes.AddRange(viewNodeList.ToArray());
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            var nodeList = treeView.Nodes;
            if (nodeList == null || nodeList.Count == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(m_strCurrentEditFilePath))
            {
                // open 
            }
            BTNodeParser.Instance.SavePlan(m_strCurrentEditFilePath, m_Controller.ConvertViewNodeListToDataNodeList(nodeList));
        }
        private void 展开全部子节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 添加节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNode();
        }
        private void 编辑节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditNode(treeView.SelectedNode as CustomViewNode);
        }
        private void 删除节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        { 
            // check config
            if (BTNodeTypeManager.Instance.GetOptionTypeList() == null ||
                BTNodeTypeManager.Instance.GetOptionTypeList().Count == 0)
            {
                MessageBox.Show(this, "节点配置不完善", "参数错误", MessageBoxButtons.OK);

                foreach (ToolStripMenuItem elem in contextMenuStrip1.Items)
                {
                    elem.Enabled = false;
                }
                return;
            }
            // hide menu
            foreach (ToolStripMenuItem elem in contextMenuStrip1.Items)
            {
                if (elem.Text == "编辑节点")
                {
                    elem.Enabled = treeView.SelectedNode != null;
                }
                if (elem.Text == "删除节点")
                {
                    elem.Enabled = treeView.SelectedNode != null;
                }
                if (elem.Text == "展开全部子节点")
                {
                    elem.Enabled = treeView.Nodes.Count > 0;
                }
            }
        }
        private void OnClickTreeItem(object sender, EventArgs e)
        {
            if (m_NodeEditorPanel.GetStatus() == NodePanelOpr.Add)
            {
                MessageBox.Show(this, "正在添加节点", "错误", MessageBoxButtons.OK);
                return;
            }
            if (null == treeView.SelectedNode)
            {
                return;
            }
            EditNode(treeView.SelectedNode as CustomViewNode);
        }
        private void CreateNode()
        {
            m_NodeEditorPanel.Visible = true;
            m_NodeEditorPanel.CreateNode(OnCreateNewNode);
        }
        private void EditNode(CustomViewNode node)
        {
            m_NodeEditorPanel.Visible = true;
            m_NodeEditorPanel.EditNode(node);
        }
        private void ClearPanel()
        {
            if (null != m_NodeEditorPanel)
            {
                m_NodeEditorPanel.Visible = false;
            }
        }
        private void InitPanel()
        {
            m_NodeEditorPanel = new AINodeEditorPanel();
            m_NodeEditorPanel.Location = new Point(5,20);
            groupBox1.Controls.Add(m_NodeEditorPanel);
            ClearPanel();
        }
        private void OnCreateNewNode(CustomViewNode node)
        {
            CustomViewNode root = null;
            node.Text = node.GetData().m_strName;
            if (treeView.SelectedNode != null)
            {
                root = treeView.SelectedNode as CustomViewNode;
                if (null == root.GetData().m_ChildList)
                {
                    root.GetData().m_ChildList = new List<BTNodeData>();
                }
                root.GetData().m_ChildList.Add(node.GetData());
                root.Nodes.Add(node);
            }
            else
            {
                treeView.Nodes.Add(node);
                treeView.SelectedNode = node;
            }
            ClearPanel();
        }
    }
}
