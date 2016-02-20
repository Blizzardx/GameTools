using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelImproter.Export;
using Thrift.Protocol;

namespace ExcelImproter
{
    public partial class Form1 : Form
    {
        private ConfigExport exprot;

        public Form1()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            exprot = new ConfigExport();

            ExcelReader a = new ExcelReader();
            List<string[][]> content = null;
            a.ReadExcel("D:/My Documents/Visual Studio 2013/Projects/ExcelImproter/ExcelImproter/config/TestGameConfigLevel1.xlsx", ref content);
            exprot.ExportConfig1(content);
            content = null;
            a.ReadExcel("D:/My Documents/Visual Studio 2013/Projects/ExcelImproter/ExcelImproter/config/TestGameConfigLevel2.xlsx", ref content);
            exprot.ExportConfig2(content);
            Console.WriteLine("Done");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //import mmadv 
            TBase tbase = null;
            XmlConfigBase xbase = null;
            TrunkImporter a = new TrunkImporter();
            //a.Importer("D:/My Documents/Visual Studio 2013/Projects/ExcelImproter/ExcelImproter/config/TrunkConfig.xlsx", out tbase, out xbase);
            a.Importer(SystemInfo.m_strExcelPath, out tbase, out xbase);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SystemInfo.m_strExcelPath = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            SystemInfo.m_strXmlOutputPath = textBox2.Text;
        }
    }
}
