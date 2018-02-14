namespace SharpSourceLiveAudioMixer.forms
{
    partial class MainForm
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
            if(disposing && (components != null))
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button_settings = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox_import = new System.Windows.Forms.GroupBox();
            this.button_import_youtube = new System.Windows.Forms.Button();
            this.button_import_file = new System.Windows.Forms.Button();
            this.button_playkey = new System.Windows.Forms.Button();
            this.button_start = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.importFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.button_import_netease = new System.Windows.Forms.Button();
            this.groupBox_import.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Game:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(53, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(423, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button_settings
            // 
            this.button_settings.Location = new System.Drawing.Point(482, 10);
            this.button_settings.Name = "button_settings";
            this.button_settings.Size = new System.Drawing.Size(75, 23);
            this.button_settings.TabIndex = 2;
            this.button_settings.Text = "Settings";
            this.button_settings.UseVisualStyleBackColor = true;
            this.button_settings.Click += new System.EventHandler(this.button_settings_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(12, 38);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(545, 211);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Alias";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Volumn";
            this.columnHeader3.Width = 50;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Track";
            this.columnHeader4.Width = 45;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Trim";
            this.columnHeader5.Width = 91;
            // 
            // columnHeader6
            // 
            this.columnHeader6.DisplayIndex = 0;
            this.columnHeader6.Text = "";
            this.columnHeader6.Width = 18;
            // 
            // groupBox_import
            // 
            this.groupBox_import.Controls.Add(this.button_import_netease);
            this.groupBox_import.Controls.Add(this.button_import_youtube);
            this.groupBox_import.Controls.Add(this.button_import_file);
            this.groupBox_import.Location = new System.Drawing.Point(12, 255);
            this.groupBox_import.Name = "groupBox_import";
            this.groupBox_import.Size = new System.Drawing.Size(291, 49);
            this.groupBox_import.TabIndex = 4;
            this.groupBox_import.TabStop = false;
            this.groupBox_import.Text = "Import";
            // 
            // button_import_youtube
            // 
            this.button_import_youtube.Enabled = false;
            this.button_import_youtube.Location = new System.Drawing.Point(87, 20);
            this.button_import_youtube.Name = "button_import_youtube";
            this.button_import_youtube.Size = new System.Drawing.Size(75, 23);
            this.button_import_youtube.TabIndex = 1;
            this.button_import_youtube.Text = "Youtube";
            this.button_import_youtube.UseVisualStyleBackColor = true;
            this.button_import_youtube.Click += new System.EventHandler(this.button_import_youtube_Click);
            // 
            // button_import_file
            // 
            this.button_import_file.Location = new System.Drawing.Point(6, 20);
            this.button_import_file.Name = "button_import_file";
            this.button_import_file.Size = new System.Drawing.Size(75, 23);
            this.button_import_file.TabIndex = 0;
            this.button_import_file.Text = "File";
            this.button_import_file.UseVisualStyleBackColor = true;
            this.button_import_file.Click += new System.EventHandler(this.button_import_file_Click);
            // 
            // button_playkey
            // 
            this.button_playkey.Location = new System.Drawing.Point(416, 255);
            this.button_playkey.Name = "button_playkey";
            this.button_playkey.Size = new System.Drawing.Size(141, 23);
            this.button_playkey.TabIndex = 5;
            this.button_playkey.Text = "Play Key: ";
            this.button_playkey.UseVisualStyleBackColor = true;
            this.button_playkey.Click += new System.EventHandler(this.button_playkey_Click);
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(335, 255);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 6;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(79, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(371, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "No supported game found,please check steam folder settings =>";
            this.label2.Visible = false;
            // 
            // importFileDialog
            // 
            this.importFileDialog.Multiselect = true;
            this.importFileDialog.Title = "Import Media File";
            // 
            // button_import_netease
            // 
            this.button_import_netease.Enabled = false;
            this.button_import_netease.Location = new System.Drawing.Point(168, 20);
            this.button_import_netease.Name = "button_import_netease";
            this.button_import_netease.Size = new System.Drawing.Size(117, 23);
            this.button_import_netease.TabIndex = 2;
            this.button_import_netease.Text = "Netease Music";
            this.button_import_netease.UseVisualStyleBackColor = true;
            this.button_import_netease.Click += new System.EventHandler(this.button_import_netease_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 316);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.button_playkey);
            this.Controls.Add(this.groupBox_import);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button_settings);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Sharp Source Live Audio Mixer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox_import.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button_settings;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox_import;
        private System.Windows.Forms.Button button_import_file;
        private System.Windows.Forms.Button button_playkey;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_import_youtube;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog importFileDialog;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button button_import_netease;
    }
}

