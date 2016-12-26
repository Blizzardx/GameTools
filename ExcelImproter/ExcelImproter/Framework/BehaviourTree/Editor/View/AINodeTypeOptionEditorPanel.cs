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
    public partial class AINodeTypeOptionEditorPanel : UserControl
    {
        private BTNodeTypeInfoData m_Data;
        private Action<BTNodeTypeInfoData,bool> m_OnSelectedCallback;
        private bool m_bIsLock;

        public AINodeTypeOptionEditorPanel()
        {
            InitializeComponent();
        }
        public void SetCallback(Action<BTNodeTypeInfoData, bool> callback)
        {
            m_OnSelectedCallback = callback;
        }
        public void Refresh(BTNodeTypeInfoData rootData,BTNodeTypeInfoData data)
        {
            m_Data = data;
            label1.Text = data.m_strName;
            m_bIsLock = true;
            checkBox1.Checked = false;
            if (null == rootData.m_OptionChildTypeList)
            {
                rootData.m_OptionChildTypeList = new List<string>();
            }
            for (int i = 0; i < rootData.m_OptionChildTypeList.Count; ++i)
            {
                if (rootData.m_OptionChildTypeList[i] == data.m_strName)
                {
                    checkBox1.Checked = true;
                    break;
                }
            }
            m_bIsLock = false;
        }
        public bool GetIsSelected()
        {
            return checkBox1.Checked;
        }
        public BTNodeTypeInfoData GetData()
        {
            return m_Data;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bIsLock)
            {
                return;
            }
            m_OnSelectedCallback(m_Data, checkBox1.Checked);
        }

    }
}
