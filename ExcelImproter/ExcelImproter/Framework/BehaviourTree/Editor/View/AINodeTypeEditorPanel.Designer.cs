namespace ExcelImproter.Framework.BehaviourTree.Editor.View
{
    partial class AINodeTypeEditorPanel
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.checkBoxIsLimitCount = new System.Windows.Forms.CheckBox();
            this.textBoxLimitCount = new System.Windows.Forms.TextBox();
            this.checkBoxLimitType = new System.Windows.Forms.CheckBox();
            this.checkBoxIsRoot = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(12, 6);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(140, 21);
            this.textBoxName.TabIndex = 4;
            // 
            // checkBoxIsLimitCount
            // 
            this.checkBoxIsLimitCount.AutoSize = true;
            this.checkBoxIsLimitCount.Location = new System.Drawing.Point(158, 8);
            this.checkBoxIsLimitCount.Name = "checkBoxIsLimitCount";
            this.checkBoxIsLimitCount.Size = new System.Drawing.Size(108, 16);
            this.checkBoxIsLimitCount.TabIndex = 5;
            this.checkBoxIsLimitCount.Text = "限制子节点个数";
            this.checkBoxIsLimitCount.UseVisualStyleBackColor = true;
            // 
            // textBoxLimitCount
            // 
            this.textBoxLimitCount.Location = new System.Drawing.Point(270, 6);
            this.textBoxLimitCount.Name = "textBoxLimitCount";
            this.textBoxLimitCount.Size = new System.Drawing.Size(39, 21);
            this.textBoxLimitCount.TabIndex = 6;
            this.textBoxLimitCount.TextChanged += new System.EventHandler(this.textBoxLimitCount_TextChanged);
            // 
            // checkBoxLimitType
            // 
            this.checkBoxLimitType.AutoSize = true;
            this.checkBoxLimitType.Location = new System.Drawing.Point(315, 8);
            this.checkBoxLimitType.Name = "checkBoxLimitType";
            this.checkBoxLimitType.Size = new System.Drawing.Size(108, 16);
            this.checkBoxLimitType.TabIndex = 7;
            this.checkBoxLimitType.Text = "限制子节点类型";
            this.checkBoxLimitType.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsRoot
            // 
            this.checkBoxIsRoot.AutoSize = true;
            this.checkBoxIsRoot.Location = new System.Drawing.Point(429, 8);
            this.checkBoxIsRoot.Name = "checkBoxIsRoot";
            this.checkBoxIsRoot.Size = new System.Drawing.Size(96, 16);
            this.checkBoxIsRoot.TabIndex = 8;
            this.checkBoxIsRoot.Text = "是否是根节点";
            this.checkBoxIsRoot.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(537, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "-";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(589, 8);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(15, 14);
            this.checkBox4.TabIndex = 12;
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // AINodeTypeEditorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxIsRoot);
            this.Controls.Add(this.checkBoxLimitType);
            this.Controls.Add(this.textBoxLimitCount);
            this.Controls.Add(this.checkBoxIsLimitCount);
            this.Controls.Add(this.textBoxName);
            this.Name = "AINodeTypeEditorPanel";
            this.Size = new System.Drawing.Size(612, 32);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.CheckBox checkBoxIsLimitCount;
        private System.Windows.Forms.TextBox textBoxLimitCount;
        private System.Windows.Forms.CheckBox checkBoxLimitType;
        private System.Windows.Forms.CheckBox checkBoxIsRoot;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox4;
    }
}
