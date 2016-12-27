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
        private AIEditorController  m_Controller;
        private AINodeEditorPanel   m_NodeEditorPanel;
        private string              m_strCurrentEditFilePath;
        private bool                m_bIsAddRootNode;

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
            m_strCurrentEditFilePath = OpenFile();
            if (string.IsNullOrEmpty(m_strCurrentEditFilePath))
            {
                return;
            }
            BTNodeParser.Instance.LoadBTPlan(m_strCurrentEditFilePath);
            List<CustomViewNode> viewNodeList = m_Controller.ConvertDateNodeListToViewNodeList(BTNodeParser.Instance.GetPlanList());
            if (null == viewNodeList)
            {
                return;
            }
            treeView.Nodes.Clear();
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
                m_strCurrentEditFilePath = SaveFile();
                if (string.IsNullOrEmpty(m_strCurrentEditFilePath))
                {
                    return;
                }
            }
            BTNodeParser.Instance.SavePlan(m_strCurrentEditFilePath, m_Controller.ConvertViewNodeListToDataNodeList(nodeList));
        }
        private void 展开全部子节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode == null)
            {
                treeView.ExpandAll();
            }
            else
            {
                treeView.SelectedNode.ExpandAll();
            }
        }
        private void 添加节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bIsAddRootNode = false;
            CreateNode(false);
        }
        private void 添加根节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bIsAddRootNode = true;
            CreateNode(true);
        }
        private void 删除节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode == null)
            {
                return;
            }
            var res = MessageBox.Show(this, "确定要执行操作吗？", "警告", MessageBoxButtons.OKCancel,MessageBoxIcon.Warning );
            if (res == DialogResult.OK)
            {
                // do delete
                CustomViewNode node = treeView.SelectedNode as CustomViewNode;
                if (null == node.Parent)
                {
                    treeView.Nodes.Remove(node);
                }
                else
                {
                    CustomViewNode parent = node.Parent as CustomViewNode;
                    for (int i = 0; i < parent.GetData().m_ChildList.Count; ++i)
                    {
                        if (parent.GetData().m_ChildList[i] == node.GetData())
                        {
                            parent.GetData().m_ChildList.RemoveAt(i);
                            parent.Nodes.Remove(node);
                            break;
                        }
                    }
                }
            }
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        { 
            // check config
            if (BTNodeTypeManager.Instance.GetTypeInfoList() == null ||
                BTNodeTypeManager.Instance.GetTypeInfoList().Count == 0)
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
                if (elem.Text == "添加节点")
                {
                    if (treeView.SelectedNode != null)
                    {
                        CustomViewNode node = treeView.SelectedNode as CustomViewNode;
                        if (!CheckCanAddChildNode(node.GetData().m_strType))
                        {
                            elem.Enabled = false;
                        }
                        else
                        {
                            elem.Enabled = true;
                        }
                    }
                    else
                    {
                        elem.Enabled = false;
                    }
                }
            }
        }
        private bool CheckCanAddChildNode(string nodeType)
        {
            var list = BTNodeTypeManager.Instance.GetTypeInfoList();
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i].m_strName == nodeType)
                {
                    if (list[i].m_bIsLimitChildCount && list[i].m_iLimitChildCount == 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
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
        private void CreateNode(bool isRoot)
        {
            m_NodeEditorPanel.Visible = true;
            m_NodeEditorPanel.CreateNode(isRoot,OnCreateNewNode);
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
            node.Text = node.GetData().m_strType + ":" + node.GetData().m_strName;
            if (m_bIsAddRootNode || treeView.SelectedNode == null)
            {
                if (!CheckPlanId(node))
                {
                    MessageBox.Show(this, "id already exist", "参数错误", MessageBoxButtons.OK);
                    return;
                }
                treeView.Nodes.Add(node);
                treeView.SelectedNode = node;
            }
            else
            {
                string errorMsg = string.Empty;
                root = treeView.SelectedNode as CustomViewNode;
                if (!CheckAddNodeRule(root,node,ref errorMsg))
                {
                    MessageBox.Show(this, errorMsg, "参数错误", MessageBoxButtons.OK);
                    return;
                }
                if (null == root.GetData().m_ChildList)
                {
                    root.GetData().m_ChildList = new List<BTNodeData>();
                }
                root.GetData().m_ChildList.Add(node.GetData());
                root.Nodes.Add(node);
            }

            ClearPanel();
        }
        private bool CheckPlanId(CustomViewNode node)
        {
            var roots = treeView.Nodes;
            foreach (var elem in roots)
            {
                CustomViewNode subNode = elem as CustomViewNode;
                if (subNode.GetData().m_Id == node.GetData().m_Id)
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckAddNodeRule(CustomViewNode root, CustomViewNode current,ref string errorMsg)
        {
            BTNodeTypeInfoData rootTypeInfo = null;
            BTNodeTypeInfoData nodeTypeInfo = null;

            foreach (var elem in BTNodeTypeManager.Instance.GetTypeInfoList())
            {
                if (elem.m_strName == root.GetData().m_strType)
                {
                    rootTypeInfo = elem;
                }
                if (elem.m_strName == current.GetData().m_strType)
                {
                    nodeTypeInfo = elem;
                }
            }
            if (null == nodeTypeInfo || null == rootTypeInfo)
            {
                errorMsg = "节点类型错误";
                return false;
            }
            if (rootTypeInfo.m_bIsLimitChildCount)
            {
                if (root.GetData().m_ChildList != null && root.GetData().m_ChildList.Count >= rootTypeInfo.m_iLimitChildCount)
                {
                    errorMsg = "超过了该节点允许添加的节点个数";
                    return false;
                }
            }
            if (rootTypeInfo.m_bIsLimitChildType)
            {
                bool isChildInList = false;
                for (int i = 0; i < rootTypeInfo.m_OptionChildTypeList.Count; ++i)
                {
                    if (rootTypeInfo.m_OptionChildTypeList[i] == nodeTypeInfo.m_strName)
                    {
                        isChildInList = true;
                        break;
                    }   
                }
                if (!isChildInList)
                {
                    errorMsg = "该子节点类型不允许添加到该父节点下";
                    return false;
                }
            }
            if (nodeTypeInfo.m_bIsRoot)
            {
                errorMsg = "根节点不允许被添加成子节点";
                return false;
            }
            return true;
        }
        private string OpenFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                return fileDialog.FileName;
            }
            return null;
        }
        private string OpenFolder()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            return null;
        }
        private string SaveFile()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                return fileDialog.FileName;
            }
            return null;
        }
    }
}
