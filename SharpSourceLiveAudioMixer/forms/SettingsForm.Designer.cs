namespace SharpSourceLiveAudioMixer.forms
{
    partial class SettingsForm
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
            if(disposing && (components != null))
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_steamfolder = new System.Windows.Forms.TextBox();
            this.button_scan = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Steam Folder:";
            // 
            // textBox_steamfolder
            // 
            this.textBox_steamfolder.Location = new System.Drawing.Point(101, 12);
            this.textBox_steamfolder.Name = "textBox_steamfolder";
            this.textBox_steamfolder.Size = new System.Drawing.Size(198, 21);
            this.textBox_steamfolder.TabIndex = 1;
            this.textBox_steamfolder.Leave += new System.EventHandler(this.textBox_steamfolder_Leave);
            // 
            // button_scan
            // 
            this.button_scan.Location = new System.Drawing.Point(350, 10);
            this.button_scan.Name = "button_scan";
            this.button_scan.Size = new System.Drawing.Size(99, 23);
            this.button_scan.TabIndex = 2;
            this.button_scan.Text = "Scan Games";
            this.button_scan.UseVisualStyleBackColor = true;
            this.button_scan.Click += new System.EventHandler(this.button_scan_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(305, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(39, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 45);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_scan);
            this.Controls.Add(this.textBox_steamfolder);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_steamfolder;
        private System.Windows.Forms.Button button_scan;
        private System.Windows.Forms.Button button1;
    }
}