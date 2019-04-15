namespace Keyword
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cbbKeyword = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbbKeyword
            // 
            this.cbbKeyword.FormattingEnabled = true;
            this.cbbKeyword.Location = new System.Drawing.Point(13, 5);
            this.cbbKeyword.Name = "cbbKeyword";
            this.cbbKeyword.Size = new System.Drawing.Size(483, 20);
            this.cbbKeyword.TabIndex = 0;
            this.cbbKeyword.TextUpdate += new System.EventHandler(this.cbbKeyword_TextUpdate);
            this.cbbKeyword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbbKeyword_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 30);
            this.Controls.Add(this.cbbKeyword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbKeyword;
    }
}

