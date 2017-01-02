using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

        public ExcelTitleParamEditor()
        {
            InitializeComponent();
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }
        private void buttonDone_Click(object sender, EventArgs e)
        {

        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
        public NodePanelOpr GetStatus()
        {
            return m_Status;
        }
    }
}
