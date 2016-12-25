using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ExcelImporter.Importer;
using ExcelImproter.Framework.BehaviourTree;
using ExcelImproter.Framework.BehaviourTree.Editor;
using ExcelImproter.Framework.BehaviourTree.Editor.Controller;
using ExcelImproter.Framework.Handler;

namespace ExcelImproter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RefreshFileList();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string log = LogQueue.Instance.Dequeue();
            if (log == null)
            {
                return;
            }
            this.richTextBox1.AppendText(log);
            this.richTextBox1.Focus();
            this.richTextBox1.Select(this.richTextBox1.Text.Length, 0);
            this.richTextBox1.ScrollToCaret();
        }
        private void 自动生成解析代码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScriptGenTool.GenAllScript();
        }
        private void 测试按钮ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ManualHandlerManager.Instance.HandleConfig("diyCharConfig.xlsx", "E:/Project/GameClient/策划文档/config/dev/cqq/1.1/");
            List<string> list = new List<string>()
            {
                "BTRoot",
                "BTCondition",
                "BTSequence",
                "BTSelector",
                "BTDecorate",
                "BTActionIdle",
            };
            BTNodeTypeManager.Instance.SaveTypeList(BTConfigSetting.BTNodeTypeConfigPath, list);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            RefreshFileList();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var list = HandlerCommon.Instance.GetAllReadyConfig();
            string name = comboBox1.SelectedItem as string;
            for (int i = 0; i < list.Count; ++i)
            {
                if (name == list[i].m_strName)
                {
                    ManualHandlerManager.Instance.HandleConfig(list[i].m_strName,list[i].m_strSubPath);
                    break;
                }
            }
        }
        private void button3_Click(object sender, EventArgs e1)
        {
        }
        private void RefreshFileList()
        {
            HandlerCommon.Instance.SetConfigFolderPath("E:/Project/GameClient/策划文档/config/dev/cqq/1.1/");
            HandlerCommon.Instance.Refresh();
            var list = HandlerCommon.Instance.GetAllReadyConfig();
            comboBox1.Items.Clear();
            for (int i = 0; i < list.Count; ++i)
            {
                comboBox1.Items.Add(list[i].m_strName);
            }
            if (comboBox1.Items.Count != 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }
        private void aI编辑器ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormCollection coll = Application.OpenForms;
            foreach (Form form in coll)
            {
                if (form is AIEditorForm)
                {
                    form.Focus();
                    return;
                }
            }
            AIEditorForm aiForm = new AIEditorForm();
            aiForm.Show();
        }
    }
}
