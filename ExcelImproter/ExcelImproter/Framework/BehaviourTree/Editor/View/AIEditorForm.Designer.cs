namespace ExcelImproter.Framework.BehaviourTree.Editor
{
    partial class AIEditorForm
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
            this.components = new System.ComponentModel.Container();
            this.treeView = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.展开全部子节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加根节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView.Location = new System.Drawing.Point(6, 71);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(992, 608);
            this.treeView.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.展开全部子节点ToolStripMenuItem,
            this.添加根节点ToolStripMenuItem,
            this.添加节点ToolStripMenuItem,
            this.删除节点ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(159, 92);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 展开全部子节点ToolStripMenuItem
            // 
            this.展开全部子节点ToolStripMenuItem.Name = "展开全部子节点ToolStripMenuItem";
            this.展开全部子节点ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.展开全部子节点ToolStripMenuItem.Text = "展开全部子节点";
            this.展开全部子节点ToolStripMenuItem.Click += new System.EventHandler(this.展开全部子节点ToolStripMenuItem_Click);
            // 
            // 添加根节点ToolStripMenuItem
            // 
            this.添加根节点ToolStripMenuItem.Name = "添加根节点ToolStripMenuItem";
            this.添加根节点ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.添加根节点ToolStripMenuItem.Text = "添加跟节点";
            this.添加根节点ToolStripMenuItem.Click += new System.EventHandler(this.添加根节点ToolStripMenuItem_Click);
            // 
            // 添加节点ToolStripMenuItem
            // 
            this.添加节点ToolStripMenuItem.Name = "添加节点ToolStripMenuItem";
            this.添加节点ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.添加节点ToolStripMenuItem.Text = "添加节点";
            this.添加节点ToolStripMenuItem.Click += new System.EventHandler(this.添加节点ToolStripMenuItem_Click);
            // 
            // 删除节点ToolStripMenuItem
            // 
            this.删除节点ToolStripMenuItem.Name = "删除节点ToolStripMenuItem";
            this.删除节点ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.删除节点ToolStripMenuItem.Text = "删除节点";
            this.删除节点ToolStripMenuItem.Click += new System.EventHandler(this.删除节点ToolStripMenuItem_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(197, 13);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(102, 36);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(12, 13);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(102, 36);
            this.buttonLoad.TabIndex = 2;
            this.buttonLoad.Text = "载入";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(1007, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(557, 606);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "节点详情";
            // 
            // AIEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1573, 685);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.treeView);
            this.Name = "AIEditorForm";
            this.Text = "AIEditorForm";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 展开全部子节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加根节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除节点ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}