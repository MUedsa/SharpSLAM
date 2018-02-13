using System.Windows.Forms;

namespace SharpSourceLiveAudioMixer.forms
{
    public partial class ProcessingDialog : Form
    {
        public ProcessingDialog(int current,int max)
        {
            InitializeComponent();
            update(current,max);
        }

        public void update(int current = -1,int max = -1)
        {
            if(max != -1)
            {
                progressBar1.Maximum = max;
            }
            if(current == -1)
            {
                current = progressBar1.Value + 1;
            }
            if(current > progressBar1.Maximum)
            {
                Close();
                return;
            }
            progressBar1.Value = current;
            label1.Text = "Processing...(" + progressBar1.Value + "/" + progressBar1.Maximum + ")";
        }
    }
}
