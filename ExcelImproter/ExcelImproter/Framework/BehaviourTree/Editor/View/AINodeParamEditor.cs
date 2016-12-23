using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelImproter.Framework.BehaviourTree.Editor
{
    public partial class AINodeParamEditor : UserControl
    {
        public AINodeParamEditor()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            var types = BTNodeParamDataTypeDesc.BTNodeParamDataTypes;
            for (int i = 0; i < types.Length; ++i)
            {
                comboBox1.Items.Add(types[i]);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
