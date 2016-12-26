using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExcelImproter.Framework.BehaviourTree.Editor.Controller;

namespace ExcelImproter.Framework.BehaviourTree.Editor.View
{
    public partial class AINodeTypeParamterEditorPanel : UserControl
    {
        private BTNodeTypeParamterData          m_Data;
        private Action<BTNodeTypeParamterData>  m_OnSubCallback;
        public AINodeTypeParamterEditorPanel()
        {
            InitializeComponent();
            var types = BTNodeParamDataTypeDesc.BTNodeParamDataTypes;
            for (int i = 0; i < types.Length; ++i)
            {
                comboBox1.Items.Add(types[i]);
            }
            comboBox1.SelectedIndex = 0;
        }
        public void SetCallback(Action<BTNodeTypeParamterData> callback)
        {
            m_OnSubCallback = callback;
        }
        public void Refresh(BTNodeTypeParamterData data)
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
        }
        public BTNodeTypeParamterData GetData()
        {
            return m_Data;
        }
        public void Save()
        {
            m_Data.m_strName = textBoxName.Text;
            m_Data.m_Type = (BTNodeParamDataType)comboBox1.SelectedItem;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            m_OnSubCallback(m_Data);
        }

    }
}
