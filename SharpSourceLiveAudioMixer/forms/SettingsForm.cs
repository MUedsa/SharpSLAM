using System;
using System.Windows.Forms;

namespace SharpSourceLiveAudioMixer.forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            textBox_steamfolder.Text = Program.Config["SteamFolder"];
            label3.Text = Program.Config["ProxyKey","F1"];
            checkBox1.Checked = Program.Config.GetBool("HoldToPlay",false);
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

        private void button2_Click(object sender,EventArgs e)
        {
            var dialog = new SelectKeyDialog(Program.Config["ProxyKey"]);
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                label3.Text = Program.Config["ProxyKey"] = dialog.Result;
                Program.Config.Save();
            }
        }

        private void checkBox1_CheckedChanged(object sender,EventArgs e)
        {
            Program.Config["HoldToPlay"] = checkBox1.Checked.ToString();
        }
    }
}
