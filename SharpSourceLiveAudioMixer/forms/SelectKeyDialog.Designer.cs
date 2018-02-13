namespace SharpSourceLiveAudioMixer.forms
{
    partial class SelectKeyDialog
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "F6",
            "F7",
            "F8",
            "F9",
            "F10",
            "F11",
            "F12",
            "\'",
            "-",
            ",",
            ".",
            "/",
            "[",
            "\\",
            "]",
            "`",
            "=",
            "ALT",
            "BACKSPACE",
            "CAPSLOCK",
            "CTRL",
            "DEL",
            "DOWNARROW",
            "END",
            "ENTER",
            "ESCAPE",
            "HOME",
            "INS",
            "KP_5",
            "KP_DEL",
            "KP_DOWNARROW",
            "KP_END",
            "KP_ENTER",
            "KP_HOME",
            "KP_INS",
            "KP_LEFTARROW",
            "KP_MINUS",
            "KP_MULTIPLY",
            "KP_PGDN",
            "KP_PGUP",
            "KP_PLUS",
            "KP_RIGHTARROW",
            "KP_SLASH",
            "KP_UPARROW",
            "LEFTARROW",
            "LWIN",
            "MOUSE1",
            "MOUSE2",
            "MOUSE3",
            "MOUSE4",
            "MOUSE5",
            "MWHEELDOWN",
            "MWHEELUP",
            "NUMLOCK",
            "PGDN",
            "PGUP",
            "SCRO",
            "RCTRL",
            "RIGHTARROW",
            "RSHIFT",
            "RWINLLOCK",
            "SEMICOLON",
            "SHIFT",
            "SPACE",
            "TAB",
            "UPARROW"});
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(224, 20);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(242, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Done";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select or type a key name works with bind command.";
            // 
            // SelectKeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 56);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectKeyForm";
            this.Text = "Select Key";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}