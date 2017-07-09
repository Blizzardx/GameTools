using ExcelImproter.Project;
using System;
using System.Windows.Forms;

namespace ExcelImproter
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {    
            InitializeComponent();
            RefreshFileList();

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
        private void button2_Click(object sender, EventArgs e)
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
            ConfigHandlerManager.Instance.SetConfigFolderPath("E:/Project/GameClient/策划文档/config/dev/cqq/1.1/");
            
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
    }
}
