using System;
using System.Windows.Forms;
using ExcelImproter.Framework.BehaviourTree.Editor.Controller;

namespace ExcelImproter.Framework.BehaviourTree.Editor
{
    public partial class AINodeParamEditor : UserControl
    {
        private Action<BTNodeParamData> m_OperCallback;
        private BTNodeParamData m_Data;

        public AINodeParamEditor()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            var types = BTNodeParamDataTypeDesc.BTNodeParamDataTypes;
            for (int i = 0; i < types.Length; ++i)
            {
                comboBox1.Items.Add(types[i]);
            }
            comboBox1.SelectedIndex = 0;
        }
        public void Refresh(BTNodeParamData data)
        {
            m_Data = data;
            for (int i = 0; i < comboBox1.Items.Count; ++i)
            {
                BTNodeParamDataType item = (BTNodeParamDataType)comboBox1.Items[i];
                if (item == m_Data.m_Type)
                {
                    comboBox1.SelectedIndex = i;
                    break;
                }
            }
            if (string.IsNullOrEmpty(m_Data.m_strName))
            {
                m_Data.m_strName = string.Empty;
            }
            textBoxName.Text = m_Data.m_strName;

            if (string.IsNullOrEmpty(m_Data.m_Value))
            {
                m_Data.m_Value = string.Empty;
            }
            textBoxValue.Text = m_Data.m_Value;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            m_OperCallback(m_Data);
        }
        public void SetCallback(Action<BTNodeParamData> callback)
        {
            m_OperCallback = callback;
        }
        public void Save()
        {
            m_Data.m_strName = textBoxName.Text;
            m_Data.m_Value = textBoxValue.Text;
            m_Data.m_Type = (BTNodeParamDataType)comboBox1.SelectedItem;
        }
    }
}
