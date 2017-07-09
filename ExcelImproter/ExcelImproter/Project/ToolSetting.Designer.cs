namespace ExcelImproter.Project
{
    partial class ToolSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectXmlPathButton = new System.Windows.Forms.Button();
            this.XmlPathTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.selectExcelPathButton = new System.Windows.Forms.Button();
            this.ExcelPathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selectParserPathButton = new System.Windows.Forms.Button();
            this.ParserPathTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.configPathFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // selectXmlPathButton
            // 
            this.selectXmlPathButton.Location = new System.Drawing.Point(492, 206);
            this.selectXmlPathButton.Name = "selectXmlPathButton";
            this.selectXmlPathButton.Size = new System.Drawing.Size(70, 23);
            this.selectXmlPathButton.TabIndex = 81;
            this.selectXmlPathButton.Text = "选择目录";
            this.selectXmlPathButton.UseVisualStyleBackColor = true;
            this.selectXmlPathButton.Click += new System.EventHandler(this.selectXmlPathButton_Click);
            // 
            // XmlPathTextBox
            // 
            this.XmlPathTextBox.Location = new System.Drawing.Point(133, 210);
            this.XmlPathTextBox.Name = "XmlPathTextBox";
            this.XmlPathTextBox.ReadOnly = true;
            this.XmlPathTextBox.Size = new System.Drawing.Size(353, 21);
            this.XmlPathTextBox.TabIndex = 80;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 23);
            this.label3.TabIndex = 79;
            this.label3.Text = "Xml文件路径:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // selectExcelPathButton
            // 
            this.selectExcelPathButton.Location = new System.Drawing.Point(492, 117);
            this.selectExcelPathButton.Name = "selectExcelPathButton";
            this.selectExcelPathButton.Size = new System.Drawing.Size(70, 23);
            this.selectExcelPathButton.TabIndex = 84;
            this.selectExcelPathButton.Text = "选择目录";
            this.selectExcelPathButton.UseVisualStyleBackColor = true;
            this.selectExcelPathButton.Click += new System.EventHandler(this.selectExcelPathButton_Click);
            // 
            // ExcelPathTextBox
            // 
            this.ExcelPathTextBox.Location = new System.Drawing.Point(133, 121);
            this.ExcelPathTextBox.Name = "ExcelPathTextBox";
            this.ExcelPathTextBox.ReadOnly = true;
            this.ExcelPathTextBox.Size = new System.Drawing.Size(353, 21);
            this.ExcelPathTextBox.TabIndex = 83;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 23);
            this.label1.TabIndex = 82;
            this.label1.Text = "Excel文件路径:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // selectParserPathButton
            // 
            this.selectParserPathButton.Location = new System.Drawing.Point(492, 162);
            this.selectParserPathButton.Name = "selectParserPathButton";
            this.selectParserPathButton.Size = new System.Drawing.Size(70, 23);
            this.selectParserPathButton.TabIndex = 87;
            this.selectParserPathButton.Text = "选择目录";
            this.selectParserPathButton.UseVisualStyleBackColor = true;
            this.selectParserPathButton.Click += new System.EventHandler(this.selectParserPathButton_Click);
            // 
            // ParserPathTextBox
            // 
            this.ParserPathTextBox.Location = new System.Drawing.Point(133, 166);
            this.ParserPathTextBox.Name = "ParserPathTextBox";
            this.ParserPathTextBox.ReadOnly = true;
            this.ParserPathTextBox.Size = new System.Drawing.Size(353, 21);
            this.ParserPathTextBox.TabIndex = 86;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 23);
            this.label2.TabIndex = 85;
            this.label2.Text = "解析器文件路径:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ToolSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 462);
            this.Controls.Add(this.selectParserPathButton);
            this.Controls.Add(this.ParserPathTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectExcelPathButton);
            this.Controls.Add(this.ExcelPathTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectXmlPathButton);
            this.Controls.Add(this.XmlPathTextBox);
            this.Controls.Add(this.label3);
            this.Name = "ToolSetting";
            this.Text = "ToolSetting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectXmlPathButton;
        private System.Windows.Forms.TextBox XmlPathTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button selectExcelPathButton;
        private System.Windows.Forms.TextBox ExcelPathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button selectParserPathButton;
        private System.Windows.Forms.TextBox ParserPathTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog configPathFolderBrowserDialog;
    }
}