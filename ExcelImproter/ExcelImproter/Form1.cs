using Common.Config;
using ExcelImproter.Project;
using ExcelImproter.Project.DynamicCompile;
using ExcelImproter.Project.GenCode;
using System;
using System.IO;
using System.Windows.Forms;

namespace ExcelImproter
{
    public partial class Form1 : Form
    {
        private bool isDebug = false;

        public Form1(string[] args)
        {
            isDebug = args != null && args.Length > 0 && args[0] == "Debug";

            InitializeComponent();
            LoadSystemConfig();
            LogQueue.Instance.Enqueue(Environment.CurrentDirectory.ToString());
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
        private void buttonRefreshHandlerListClick(object sender, EventArgs e)
        {
            RefreshFileList();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void buttonImporter_Click(object sender, EventArgs e1)
        {
            ImportConfig();
        }

        private void buttonImportAll_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "确定要导入全部配置", "询问");
            if (result == DialogResult.OK)
            {
                ImportAllConfig();
            }
        }

        #region handler
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
        private void ImportConfig()
        {
            if (isDebug)
            {
                ImportConfig_Debug();
            }
            else
            {
                ImprotConfig_Release();
            }
        }
        private void ImportAllConfig()
        {
            if (isDebug)
            {
                ImportAllConfig_Debug();
            }
            else
            {
                ImprotAllConfig_Release();
            }
        }
        private void RefreshFileList()
        {
            if (isDebug)
            {
                RefreshFileList_Debug();
            }
            else
            {
                RefreshFileList_Release();
            }
        }
        #endregion

        #region release

        private void ImprotAllConfig_Release()
        {
            var list = ConfigHandlerManager.Instance.RefreshAllVaildConfigHandlerList();
            for (int i = 0; i < list.Count; ++i)
            {
                var errorInfo = ConfigHandlerManager.Instance.HandleConfig(list[i]);

                if (!string.IsNullOrEmpty(errorInfo))
                {
                    LogQueue.Instance.Enqueue(errorInfo);
                }
            }
        }
        private void ImprotConfig_Release()
        {
            var list = ConfigHandlerManager.Instance.RefreshAllVaildConfigHandlerList();
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
        private void RefreshFileList_Release()
        {
            DynamicCompiler.Instance.CompileClassAtDirectory(SystemConst.Config.CodeConfigPath);
            var list = ConfigHandlerManager.Instance.RefreshAllVaildConfigHandlerList();
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
        #endregion

        #region Debug

        private void ImportAllConfig_Debug()
        {
            var list = ConfigHandlerManager.Instance.RefreshAllVaildConfigHandlerList();
            for (int i = 0; i < list.Count; ++i)
            {
                var errorInfo = ConfigHandlerManager.Instance.HandleConfig(list[i]);

                if (!string.IsNullOrEmpty(errorInfo))
                {
                    LogQueue.Instance.Enqueue(errorInfo);
                }
            }
        }
        private void ImportConfig_Debug()
        {
            var list = ConfigHandlerManager.Instance.RefreshAllVaildConfigHandlerList();
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
        private void RefreshFileList_Debug()
        {
            var list = ConfigHandlerManager.Instance.RefreshAllVaildConfigHandlerList();
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
        private void genCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!isDebug)
            {
                return;
            }
            GenImporterCode tool = new GenImporterCode();
            tool.GenAutoImporterCode();
            LogQueue.Instance.Enqueue("Done gen code");
        }
        #endregion
    }
}
