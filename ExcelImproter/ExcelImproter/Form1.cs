using Common.Config;
using ExcelImproter.Project;
using ExcelImproter.Project.GenCode;
using System;
using System.IO;
using System.Windows.Forms;

namespace ExcelImproter
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {    
            InitializeComponent();
            RefreshFileList();
            LoadSystemConfig();
            LogQueue.Instance.Enqueue(Environment.CurrentDirectory.ToString());
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
        private void button1_Click(object sender, EventArgs e)
        {
            RefreshFileList();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void button3_Click(object sender, EventArgs e1)
        {
            var list = ConfigHandlerManager.Instance.GetAllVaildConfigHandlerList();
            string name = comboBox1.SelectedItem as string;
            for (int i = 0; i < list.Count; ++i)
            {
                if (name == list[i])
                {
                    var errorInfo = ConfigHandlerManager.Instance.HandleConfig(list[i]);

                    if (!string.IsNullOrEmpty(errorInfo))
                    {
                        LogQueue.Instance.Enqueue(errorInfo);
                    }
                    break;
                }
            }
        }
        private void RefreshFileList()
        {
            var list = ConfigHandlerManager.Instance.GetAllVaildConfigHandlerList();
            comboBox1.Items.Clear();
            for (int i = 0; i < list.Count; ++i)
            {
                comboBox1.Items.Add(list[i]);
            }
            if (comboBox1.Items.Count != 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }
        private void LoadSystemConfig()
        {
            try
            {
                var content = File.ReadAllText(SystemConst.settingConfigPath);
                SystemConst.Config = XmlConfigBase.DeSerialize<PathConfig>(content);
            }
            catch (Exception e)
            {
                SystemConst.Config = new PathConfig();
            }
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCollection coll = Application.OpenForms;
            foreach (Form form in coll)
            {
                if (form is ToolSetting)
                {
                    form.Focus();
                    return;
                }
            }
            ToolSetting settingForm = new ToolSetting();
            settingForm.Show();
        }

        private void genCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenImporterCode tool = new GenImporterCode();
            tool.GenAutoImporterCode();
        }
    }
}
