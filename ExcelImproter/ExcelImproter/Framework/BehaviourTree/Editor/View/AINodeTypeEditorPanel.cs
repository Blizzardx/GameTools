using System;
using System.Windows.Forms;
using ExcelImproter.Framework.BehaviourTree.Editor.Controller;

namespace ExcelImproter.Framework.BehaviourTree.Editor.View
{
    public partial class AINodeTypeEditorPanel : UserControl
    {
        private BTNodeTypeInfoData m_Data;
        private Action<BTNodeTypeInfoData> m_Callback;
        private Action<BTNodeTypeInfoData> m_SelectedCallback;

        public AINodeTypeEditorPanel()
        {
            InitializeComponent();
            this.Click += OnSelect;
        }
        public BTNodeTypeInfoData GetData()
        {
            return m_Data;
        }
        public void SetAsUnSelected()
        {
            checkBox4.CheckState = CheckState.Unchecked;
        }
        public void Refresh(BTNodeTypeInfoData data)
        {
            m_Data = data;
            textBoxName.Text = m_Data.m_strName;
            textBoxLimitCount.Text = m_Data.m_iLimitChildCount.ToString();
            checkBoxIsLimitCount.Checked = m_Data.m_bIsLimitChildCount;
            checkBoxLimitType.Checked = m_Data.m_bIsLimitChildType;
            checkBoxIsRoot.Checked = m_Data.m_bIsRoot;
        }
        public void Save()
        {
            m_Data.m_strName = textBoxName.Text;
            m_Data.m_iLimitChildCount = int.Parse(textBoxLimitCount.Text);
            m_Data.m_bIsLimitChildCount = checkBoxIsLimitCount.Checked;
            m_Data.m_bIsLimitChildType = checkBoxLimitType.Checked;
            m_Data.m_bIsRoot = checkBoxIsRoot.Checked;
        }
        public void SetCallback(Action<BTNodeTypeInfoData> subCallbac,Action<BTNodeTypeInfoData> selectedCallback)
        {
            m_Callback = subCallbac;
            m_SelectedCallback = selectedCallback;
        }
        private void OnSelect(object sender, EventArgs e)
        {
            checkBox4.CheckState = CheckState.Checked;
            m_SelectedCallback(m_Data);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            m_Callback(m_Data);
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                m_SelectedCallback(m_Data);
            }
        }
        private void textBoxLimitCount_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxLimitCount.Text, out m_Data.m_iLimitChildCount))
            {
                textBoxLimitCount.Text = "0";
            }
        }
    }
}
