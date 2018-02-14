using System;
using System.Windows.Forms;

namespace SharpSourceLiveAudioMixer.dialogs
{
    public partial class SetAliasDialog : Form
    {
        public string Result = null;

        public SetAliasDialog(string data)
        {
            InitializeComponent();
            textBox1.Text = data;
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
            Result = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
