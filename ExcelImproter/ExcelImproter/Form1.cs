using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelImporter.Importer;
using ExcelImproter.Framework.Handler;

namespace ExcelImproter
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
           
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
            HandlerManager.Instance.HandleConfig("diyCharConfig.xlsx", "E:/Project/GameClient/策划文档/config/dev/cqq/1.1/");
        }
    }
}
