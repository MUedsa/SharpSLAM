namespace SharpSourceLiveAudioMixer.forms
{
    partial class MusicPlayForm
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
            this.button_play = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_play
            // 
            this.button_play.Location = new System.Drawing.Point(161, 24);
            this.button_play.Name = "button_play";
            this.button_play.Size = new System.Drawing.Size(80, 23);
            this.button_play.TabIndex = 0;
            this.button_play.Text = "Play";
            this.button_play.UseVisualStyleBackColor = true;
            this.button_play.Click += new System.EventHandler(this.button_play_Click);
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(247, 24);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(80, 23);
            this.button_stop.TabIndex = 1;
            this.button_stop.Text = "Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 2;
            // 
            // MusicPlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 56);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.button_play);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MusicPlayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Music Player";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MusicPlayForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_play;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Label label1;
    }
}