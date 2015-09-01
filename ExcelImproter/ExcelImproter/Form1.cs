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
    }
}
