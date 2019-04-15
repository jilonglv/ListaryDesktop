namespace Configurator
{
    partial class Configurator
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
            this.start_on_boot_button = new System.Windows.Forms.Button();
            this.running_text = new System.Windows.Forms.Label();
            this.start_on_boot_text = new System.Windows.Forms.Label();
            this.running_button = new System.Windows.Forms.Button();
            this.layout_refresher = new System.Windows.Forms.Timer(this.components);
            this.txtProgram = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.chkStartup = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // start_on_boot_button
            // 
            this.start_on_boot_button.Location = new System.Drawing.Point(108, 86);
            this.start_on_boot_button.Name = "start_on_boot_button";
            this.start_on_boot_button.Size = new System.Drawing.Size(122, 29);
            this.start_on_boot_button.TabIndex = 0;
            this.start_on_boot_button.TabStop = false;
            this.start_on_boot_button.UseVisualStyleBackColor = true;
            this.start_on_boot_button.Click += new System.EventHandler(this.start_on_boot_button_Click);
            // 
            // running_text
            // 
            this.running_text.AutoSize = true;
            this.running_text.Location = new System.Drawing.Point(10, 56);
            this.running_text.Name = "running_text";
            this.running_text.Size = new System.Drawing.Size(59, 12);
            this.running_text.TabIndex = 1;
            this.running_text.Text = "Running: ";
            // 
            // start_on_boot_text
            // 
            this.start_on_boot_text.AutoSize = true;
            this.start_on_boot_text.Location = new System.Drawing.Point(10, 94);
            this.start_on_boot_text.Name = "start_on_boot_text";
            this.start_on_boot_text.Size = new System.Drawing.Size(95, 12);
            this.start_on_boot_text.TabIndex = 2;
            this.start_on_boot_text.Text = "Start on boot: ";
            // 
            // running_button
            // 
            this.running_button.Location = new System.Drawing.Point(108, 47);
            this.running_button.Name = "running_button";
            this.running_button.Size = new System.Drawing.Size(122, 29);
            this.running_button.TabIndex = 3;
            this.running_button.TabStop = false;
            this.running_button.UseVisualStyleBackColor = true;
            this.running_button.Click += new System.EventHandler(this.running_button_Click);
            // 
            // layout_refresher
            // 
            this.layout_refresher.Enabled = true;
            this.layout_refresher.Interval = 3000;
            this.layout_refresher.Tick += new System.EventHandler(this.layout_refresher_Tick);
            // 
            // txtProgram
            // 
            this.txtProgram.Location = new System.Drawing.Point(108, 15);
            this.txtProgram.Name = "txtProgram";
            this.txtProgram.Size = new System.Drawing.Size(122, 21);
            this.txtProgram.TabIndex = 4;
            this.txtProgram.Text = "ListaryDesktop";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Program:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(109, 123);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(122, 23);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "重新监控";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // chkStartup
            // 
            this.chkStartup.AutoSize = true;
            this.chkStartup.Checked = true;
            this.chkStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStartup.Location = new System.Drawing.Point(12, 124);
            this.chkStartup.Name = "chkStartup";
            this.chkStartup.Size = new System.Drawing.Size(66, 16);
            this.chkStartup.TabIndex = 7;
            this.chkStartup.Text = "StartUp";
            this.chkStartup.UseVisualStyleBackColor = true;
            // 
            // Configurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 152);
            this.Controls.Add(this.chkStartup);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtProgram);
            this.Controls.Add(this.running_button);
            this.Controls.Add(this.start_on_boot_text);
            this.Controls.Add(this.running_text);
            this.Controls.Add(this.start_on_boot_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Configurator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurator";
            this.Load += new System.EventHandler(this.Configurator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start_on_boot_button;
        private System.Windows.Forms.Label running_text;
        private System.Windows.Forms.Label start_on_boot_text;
        private System.Windows.Forms.Button running_button;
        private System.Windows.Forms.Timer layout_refresher;
        private System.Windows.Forms.TextBox txtProgram;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.CheckBox chkStartup;
    }
}

