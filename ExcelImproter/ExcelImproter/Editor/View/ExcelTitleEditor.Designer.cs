namespace ExcelImproter.Editor
{
    partial class ExcelTitleEditor
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.treeView = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.展开全部子节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddRootNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonGenCode = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(1009, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(557, 606);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "节点详情";
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(14, 9);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(102, 36);
            this.buttonLoad.TabIndex = 6;
            this.buttonLoad.Text = "载入";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(199, 9);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(102, 36);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // treeView
            // 
            this.treeView.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView.Location = new System.Drawing.Point(8, 67);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(992, 608);
            this.treeView.TabIndex = 4;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.展开全部子节点ToolStripMenuItem,
            this.AddRootNodeToolStripMenuItem,
            this.AddNodeToolStripMenuItem,
            this.DeleteNodeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 114);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 展开全部子节点ToolStripMenuItem
            // 
            this.展开全部子节点ToolStripMenuItem.Name = "展开全部子节点ToolStripMenuItem";
            this.展开全部子节点ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.展开全部子节点ToolStripMenuItem.Text = "展开全部子节点";
            // 
            // AddRootNodeToolStripMenuItem
            // 
            this.AddRootNodeToolStripMenuItem.Name = "AddRootNodeToolStripMenuItem";
            this.AddRootNodeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.AddRootNodeToolStripMenuItem.Text = "添加跟节点";
            this.AddRootNodeToolStripMenuItem.Click += new System.EventHandler(this.AddRootNodeToolStripMenuItem_Click);
            // 
            // AddNodeToolStripMenuItem
            // 
            this.AddNodeToolStripMenuItem.Name = "AddNodeToolStripMenuItem";
            this.AddNodeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.AddNodeToolStripMenuItem.Text = "添加节点";
            this.AddNodeToolStripMenuItem.Click += new System.EventHandler(this.AddNodeToolStripMenuItem_Click);
            // 
            // DeleteNodeToolStripMenuItem
            // 
            this.DeleteNodeToolStripMenuItem.Name = "DeleteNodeToolStripMenuItem";
            this.DeleteNodeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.DeleteNodeToolStripMenuItem.Text = "删除节点";
            this.DeleteNodeToolStripMenuItem.Click += new System.EventHandler(this.DeleteNodeToolStripMenuItem_Click);
            // 
            // buttonGenCode
            // 
            this.buttonGenCode.Location = new System.Drawing.Point(396, 9);
            this.buttonGenCode.Name = "buttonGenCode";
            this.buttonGenCode.Size = new System.Drawing.Size(102, 36);
            this.buttonGenCode.TabIndex = 8;
            this.buttonGenCode.Text = "生成thrift文件";
            this.buttonGenCode.UseVisualStyleBackColor = true;
            this.buttonGenCode.Click += new System.EventHandler(this.buttonGenCode_Click);
            // 
            // ExcelTitleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1574, 685);
            this.Controls.Add(this.buttonGenCode);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.treeView);
            this.Name = "ExcelTitleEditor";
            this.Text = "ExcelTitleEditor";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 展开全部子节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddRootNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteNodeToolStripMenuItem;
        private System.Windows.Forms.Button buttonGenCode;
    }
}