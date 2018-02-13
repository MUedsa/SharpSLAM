using System;
using System.IO;
using System.Windows.Forms;

namespace SharpSourceLiveAudioMixer.forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            textBox_steamfolder.Text = Program.Config["SteamFolder"];
        }

        private void button_scan_Click(object sender,EventArgs e)
        {
            MainForm.instance.scanGames();
        }

        private void textBox_steamfolder_Leave(object sender = null,EventArgs e = null)
        {
            Program.Config["SteamFolder"] = textBox_steamfolder.Text;
            Program.Config.Save();
        }

        private void button1_Click(object sender,EventArgs e)
        {
            var dialog = new FolderBrowserDialog
            {
                Description = "Select steam folder,e.g. \"C:\\Program Files (x86)\\Steam\\\"",
                ShowNewFolderButton = false
            };
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                textBox_steamfolder.Text = dialog.SelectedPath;
                textBox_steamfolder_Leave();
            }
        }
    }
}
