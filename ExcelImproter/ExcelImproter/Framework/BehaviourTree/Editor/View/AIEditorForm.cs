using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ExcelImproter.Framework.BehaviourTree.Editor.Controller;

namespace ExcelImproter.Framework.BehaviourTree.Editor
{
    public partial class AIEditorForm : Form
    {
        private AIEditorController m_Controller;
        private AINodeEditor m_NodeEditorPanel;

        public AIEditorForm()
        {
            InitializeComponent();
            m_Controller = new AIEditorController();
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            //BTNodeParser.Instance.Save("e:\\temp.txt");
            BTNodeParser.Instance.LoadBTPlan("e:\\tmp.txt");
            List<CustomViewNode> viewNodeList = m_Controller.ConvertDateNodeListToViewNodeList(BTNodeParser.Instance.GetPlanList());
            treeView.Nodes.AddRange(viewNodeList.ToArray());
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            var nodeList = treeView.Nodes;
            BTNodeParser.Instance.SavePlan("e:\\tmp.txt",m_Controller.ConvertViewNodeListToDataNodeList(nodeList));
        }
        private void 展开全部子节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 创建节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_NodeEditorPanel || m_NodeEditorPanel.IsDisposed)
            {
                m_NodeEditorPanel = new AINodeEditor();
            }
            m_NodeEditorPanel.Show();
            m_NodeEditorPanel.Refresh(treeView.SelectedNode, NodePanelOpr.Add);
        }
        private void 编辑节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == m_NodeEditorPanel || m_NodeEditorPanel.IsDisposed)
            {
                m_NodeEditorPanel = new AINodeEditor();
            }
            m_NodeEditorPanel.Show();
            m_NodeEditorPanel.Refresh(treeView.SelectedNode, NodePanelOpr.Update);
        }
        private void 删除节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
