using System;
using System.Windows.Forms;

namespace SharpSourceLiveAudioMixer.forms
{
    public partial class SelectKeyDialog : Form
    {
        public string Result = null;

        public SelectKeyDialog(string key = "")
        {
            InitializeComponent();
            comboBox1.Text = key;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if(ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void button1_Click(object sender,EventArgs e)
        {
            Result = comboBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void comboBox1_TextChanged(object sender,EventArgs e)
        {
            button1.Enabled = (comboBox1.Text = comboBox1.Text.Trim()) != "";
        }
    }
}
