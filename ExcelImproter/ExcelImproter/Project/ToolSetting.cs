using Common.Config;
using System;
using System.IO;
using System.Windows.Forms;

namespace ExcelImproter.Project
{
    public partial class ToolSetting : Form
    {

        public ToolSetting()
        {
            InitializeComponent();
            ExcelPathTextBox.Text = SystemConst.Config.ExcelConfigPath;
            ParserPathTextBox.Text = SystemConst.Config.ParserConfigPath;
            XmlPathTextBox.Text = SystemConst.Config.XmlConfigPath;
            CodePathTextBox.Text = SystemConst.Config.CodeConfigPath;
            outputPathTextBox.Text = SystemConst.Config.OutputPath;
        }

        private void selectExcelPathButton_Click(object sender, EventArgs e)
        {
            configPathFolderBrowserDialog.SelectedPath = Environment.CurrentDirectory;
            configPathFolderBrowserDialog.Description = "选择Excel所存在的路径";
            DialogResult result = configPathFolderBrowserDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                ExcelPathTextBox.Text = configPathFolderBrowserDialog.SelectedPath;
                SystemConst.Config.ExcelConfigPath = configPathFolderBrowserDialog.SelectedPath;
                SaveSystemConfig();
            }
        }

        private void selectParserPathButton_Click(object sender, EventArgs e)
        {
            configPathFolderBrowserDialog.SelectedPath = Environment.CurrentDirectory;
            configPathFolderBrowserDialog.Description = "选择Parser所存在的路径";
            DialogResult result = configPathFolderBrowserDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                ParserPathTextBox.Text = configPathFolderBrowserDialog.SelectedPath;
                SystemConst.Config.ParserConfigPath = configPathFolderBrowserDialog.SelectedPath;
                SaveSystemConfig();
            }
        }

        private void selectXmlPathButton_Click(object sender, EventArgs e)
        {
            configPathFolderBrowserDialog.SelectedPath = Environment.CurrentDirectory;
            configPathFolderBrowserDialog.Description = "选择Xml所存在的路径";
            DialogResult result = configPathFolderBrowserDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                XmlPathTextBox.Text = configPathFolderBrowserDialog.SelectedPath;
                SystemConst.Config.XmlConfigPath = configPathFolderBrowserDialog.SelectedPath;
                SaveSystemConfig();
            }
        }
        private void selectCodePathButton_Click(object sender, EventArgs e)
        {
            configPathFolderBrowserDialog.SelectedPath = Environment.CurrentDirectory;
            configPathFolderBrowserDialog.Description = "选择代码文件所存在的路径";
            DialogResult result = configPathFolderBrowserDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                CodePathTextBox.Text = configPathFolderBrowserDialog.SelectedPath;
                SystemConst.Config.CodeConfigPath = configPathFolderBrowserDialog.SelectedPath;
                SaveSystemConfig();
            }
        }
        private void selectOutputPathButton_Click(object sender, EventArgs e)
        {
            configPathFolderBrowserDialog.SelectedPath = Environment.CurrentDirectory;
            configPathFolderBrowserDialog.Description = "选择导出文件路径";
            DialogResult result = configPathFolderBrowserDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                outputPathTextBox.Text = configPathFolderBrowserDialog.SelectedPath;
                SystemConst.Config.OutputPath = configPathFolderBrowserDialog.SelectedPath;
                SaveSystemConfig();
            }
        }
        private void SaveSystemConfig()
        {
            var content = XmlConfigBase.Serialize(SystemConst.Config);
            File.WriteAllText(SystemConst.settingConfigPath, content);
        }

    }
}
