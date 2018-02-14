using System;
using System.Windows.Forms;

namespace SharpSourceLiveAudioMixer.dialogs
{
    public partial class AdjustVolumeDialog : Form
    {
        public int Result = 0;

        public AdjustVolumeDialog()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender,EventArgs e)
        {
            label1.Text = "Volume: " + trackBar1.Value + "%";
        }

        private void button1_Click(object sender,EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Result = trackBar1.Value;
            Close();
        }
    }
}
