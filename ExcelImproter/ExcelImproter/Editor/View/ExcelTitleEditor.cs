using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ExcelImproter.Editor.Controller;
using ExcelImproter.Framework.Handler;

namespace ExcelImproter.Editor
{
    public partial class ExcelTitleEditor : Form
    {
        private ExcelTitleEditorController m_Controller;
        private ExcelTitleParamEditor m_EditorPanel;

        #region event
        public ExcelTitleEditor()
        {
            InitializeComponent();
            m_Controller = new ExcelTitleEditorController();
            treeView.AfterSelect += OnClickTreeItem;
            m_EditorPanel = new ExcelTitleParamEditor();
            groupBox1.Controls.Add(m_EditorPanel);
            Load();
        }
        private void buttonGenCode_Click(object sender, EventArgs e)
        {
            GenCode();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            Load();
        }
        private void OnClickTreeItem(object sender, EventArgs e)
        {
            if (m_EditorPanel.GetStatus() == ExcelTitleParamEditor.NodePanelOpr.Add)
            {
                MessageBox.Show(this, "正在添加节点", "错误", MessageBoxButtons.OK);
                return;
            }
            if (null == treeView.SelectedNode)
            {
                return;
            }
            EditNode(treeView.SelectedNode as ExcelTitleViewNode);
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
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
                if (elem.Text == "添加节点")
                {
                    
                }
            }
        }
        private void AddRootNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddRootNode();
        }
        private void AddNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNode(treeView.SelectedNode as ExcelTitleViewNode);
        }
        private void DeleteNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteNode(treeView.SelectedNode as ExcelTitleViewNode);
        }
        #endregion

        #region handler
        private void AddRootNode()
        {
            m_EditorPanel.AddRootNode(OnAddRootNodeDone);
        }
        private void AddNode(ExcelTitleViewNode data)
        {
            m_EditorPanel.AddNode((elem) =>
            {
                OnAddNodeDone(data, elem);
            });
        }
        private void OnAddRootNodeDone(ExcelTitleViewNode obj)
        {
            
        }
        private void OnAddNodeDone(ExcelTitleViewNode parent, ExcelTitleViewNode child)
        {
            
        }
        private void DeleteNode(ExcelTitleViewNode data)
        {
            if (data == null)
            {
                return;
            }
            var res = MessageBox.Show(this, "确定要执行操作吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (res == DialogResult.OK)
            {
                // do delete
                if (null == data.Parent)
                {
                    treeView.Nodes.Remove(data);
                }
                else
                {
                    ExcelTitleViewNode parent = data.Parent as ExcelTitleViewNode;
                    if (!(parent.GetData() is ExcelDataElement_Struct))
                    {
                        return;
                    }
                    var structInfo = parent.GetData() as ExcelDataElement_Struct;
                    for (int i = 0; i < structInfo.m_Value.Count; ++i)
                    {
                        if (structInfo.m_Value[i] == data.GetData())
                        {
                            structInfo.m_Value.RemoveAt(i);
                            parent.Nodes.Remove(data);
                            break;
                        }
                    }
                }
            }
        }
        private void EditNode(ExcelTitleViewNode data)
        {
        }
        private void Save()
        {
            List<TreeNode> list = new List<TreeNode>();
            foreach (TreeNode node in treeView.Nodes)
            {
                list.Add(node);
            }
            var info = m_Controller.ConvertViewToData(list);
            ExcelDescManager.Instance.Save(info);
        }
        private void Load()
        {
            ExcelDescManager.Instance.Load();
            var info = ExcelDescManager.Instance.GetDescList();
            treeView.Nodes.Clear();
            var nodes = m_Controller.ConvertDataToView(info);
            treeView.Nodes.AddRange(nodes.ToArray());
        }
        private void GenCode()
        {
            Save();
            GenThriftCode.Instance.GenCode(ExcelDescManager.Instance.GetDescList());
        }
        #endregion
    }
}
