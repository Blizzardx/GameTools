namespace ExcelImproter
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshHandlerList = new System.Windows.Forms.Button();
            this.buttonImporter = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 83);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(963, 514);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem,
            this.genCodeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(968, 25);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.settingToolStripMenuItem.Text = "Setting";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // genCodeToolStripMenuItem
            // 
            this.genCodeToolStripMenuItem.Name = "genCodeToolStripMenuItem";
            this.genCodeToolStripMenuItem.Size = new System.Drawing.Size(74, 21);
            this.genCodeToolStripMenuItem.Text = "GenCode";
            this.genCodeToolStripMenuItem.Click += new System.EventHandler(this.genCodeToolStripMenuItem_Click);
            // 
            // RefreshHandlerList
            // 
            this.RefreshHandlerList.Location = new System.Drawing.Point(135, 37);
            this.RefreshHandlerList.Name = "RefreshHandlerList";
            this.RefreshHandlerList.Size = new System.Drawing.Size(93, 30);
            this.RefreshHandlerList.TabIndex = 8;
            this.RefreshHandlerList.Text = "刷新文件列表";
            this.RefreshHandlerList.UseVisualStyleBackColor = true;
            this.RefreshHandlerList.Click += new System.EventHandler(this.buttonRefreshHandlerListClick);
            // 
            // buttonImporter
            // 
            this.buttonImporter.Location = new System.Drawing.Point(726, 37);
            this.buttonImporter.Name = "buttonImporter";
            this.buttonImporter.Size = new System.Drawing.Size(93, 30);
            this.buttonImporter.TabIndex = 10;
            this.buttonImporter.Text = "导入";
            this.buttonImporter.UseVisualStyleBackColor = true;
            this.buttonImporter.Click += new System.EventHandler(this.buttonImporter_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(242, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(323, 24);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 596);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.buttonImporter);
            this.Controls.Add(this.RefreshHandlerList);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button RefreshHandlerList;
        private System.Windows.Forms.Button buttonImporter;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genCodeToolStripMenuItem;
    }
}

