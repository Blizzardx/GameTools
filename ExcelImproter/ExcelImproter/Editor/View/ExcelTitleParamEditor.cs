using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExcelImproter.Editor.Controller;

namespace ExcelImproter.Editor
{
    public partial class ExcelTitleParamEditor : UserControl
    {
        public enum NodePanelOpr
        {
            Idle,
            Update,
            Add,
        }

        private NodePanelOpr m_Status;
        private bool m_bIsAddRoot;
        private Action<ExcelTitleViewNode> m_OnAddRootDone;
        private Action<ExcelTitleViewNode> m_OnAddDone;
        private ExcelTitleViewNode m_AddData;
        private Action<ExcelTitleViewNode> m_OnEditDone;
        private ExcelTitleViewNode m_EditData;

        #region event
        public ExcelTitleParamEditor()
        {
            InitializeComponent();
        }
        public NodePanelOpr GetStatus()
        {
            return m_Status;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Add();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Cancle();
        }
        private void buttonDone_Click(object sender, EventArgs e)
        {
            FixedDone();
        }
        #endregion

        #region handler add
        public void AddRootNode(Action<ExcelTitleViewNode> doneCallback)
        {
            m_Status = NodePanelOpr.Add;
            m_OnAddRootDone = doneCallback;
            m_bIsAddRoot = true;
            FixedVisable();
        }
        public void AddNode(Action<ExcelTitleViewNode> doneCallback)
        {
            m_Status = NodePanelOpr.Add;
            m_OnAddDone = doneCallback;
            m_bIsAddRoot = false;
        }

        private void FixedVisable()
        {
            foreach (Control elem in Controls)
            {
                elem.Enabled = false;
            }
            comboBoxNodeType.Enabled = true;
            labelNodeType.Enabled = true;

        }
        private void Add()
        {
            if (m_bIsAddRoot)
            {
                m_OnAddRootDone(m_AddData);
            }
            else
            {
                m_OnAddDone(m_AddData);
            }
            Visible = false;
            m_Status = NodePanelOpr.Idle;
        }
        private void Cancle()
        {
            Visible = false;
            m_Status = NodePanelOpr.Idle;
        }
        #endregion

        #region handler edit
        public void EditNode(ExcelTitleViewNode node)
        {
            m_EditData = node;
        }
        private void FixedDone()
        {
            m_OnEditDone(m_EditData);
        }
        #endregion
    }
}
