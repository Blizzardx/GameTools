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
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自动生成解析代码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试按钮ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.aI编辑器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aI编辑器ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aI编辑器设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.配置ToolStripMenuItem,
            this.aI编辑器ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(968, 25);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.自动生成解析代码ToolStripMenuItem,
            this.测试按钮ToolStripMenuItem});
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.配置ToolStripMenuItem.Text = "编辑";
            // 
            // 自动生成解析代码ToolStripMenuItem
            // 
            this.自动生成解析代码ToolStripMenuItem.Name = "自动生成解析代码ToolStripMenuItem";
            this.自动生成解析代码ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.自动生成解析代码ToolStripMenuItem.Text = "自动生成解析代码";
            this.自动生成解析代码ToolStripMenuItem.Click += new System.EventHandler(this.自动生成解析代码ToolStripMenuItem_Click);
            // 
            // 测试按钮ToolStripMenuItem
            // 
            this.测试按钮ToolStripMenuItem.Name = "测试按钮ToolStripMenuItem";
            this.测试按钮ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.测试按钮ToolStripMenuItem.Text = "测试按钮";
            this.测试按钮ToolStripMenuItem.Click += new System.EventHandler(this.测试按钮ToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(135, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 30);
            this.button1.TabIndex = 8;
            this.button1.Text = "刷新文件列表";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(576, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 30);
            this.button2.TabIndex = 9;
            this.button2.Text = "手动导入";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(726, 37);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(93, 30);
            this.button3.TabIndex = 10;
            this.button3.Text = "自动导入";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
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
            // aI编辑器ToolStripMenuItem
            // 
            this.aI编辑器ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aI编辑器ToolStripMenuItem1,
            this.aI编辑器设置ToolStripMenuItem});
            this.aI编辑器ToolStripMenuItem.Name = "aI编辑器ToolStripMenuItem";
            this.aI编辑器ToolStripMenuItem.Size = new System.Drawing.Size(32, 21);
            this.aI编辑器ToolStripMenuItem.Text = "AI";
            // 
            // aI编辑器ToolStripMenuItem1
            // 
            this.aI编辑器ToolStripMenuItem1.Name = "aI编辑器ToolStripMenuItem1";
            this.aI编辑器ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.aI编辑器ToolStripMenuItem1.Text = "AI编辑器";
            this.aI编辑器ToolStripMenuItem1.Click += new System.EventHandler(this.aI编辑器ToolStripMenuItem1_Click);
            // 
            // aI编辑器设置ToolStripMenuItem
            // 
            this.aI编辑器设置ToolStripMenuItem.Name = "aI编辑器设置ToolStripMenuItem";
            this.aI编辑器设置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aI编辑器设置ToolStripMenuItem.Text = "AI编辑器设置";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 596);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.ToolStripMenuItem 配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自动生成解析代码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试按钮ToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripMenuItem aI编辑器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aI编辑器ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aI编辑器设置ToolStripMenuItem;
    }
}

