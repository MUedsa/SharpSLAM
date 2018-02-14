using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace SharpSourceLiveAudioMixer.forms
{
    public partial class MusicPlayForm : Form
    {
        public static SoundPlayer player = new SoundPlayer();
        private static MusicPlayForm instance = new MusicPlayForm();

        public static MusicPlayForm getInstance(string path)
        {
            instance.label1.Text = Path.GetFileNameWithoutExtension(path);
            player.SoundLocation = path;
            return instance;
        }

        private MusicPlayForm()
        {
            InitializeComponent();
        }

        private void MusicPlayForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            e.Cancel = true;
            player.Stop();
            Hide();
        }

        private void button_stop_Click(object sender,EventArgs e)
        {
            player.Stop();
        }

        private void button_play_Click(object sender,EventArgs e)
        {
            player.Play();
        }
    }
}
