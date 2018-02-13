using System;
using System.Windows.Forms;

namespace SharpSourceLiveAudioMixer
{
    static class Program
    {
        public static Config Config = new Config("config/settings.ini");

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new forms.MainForm());
        }
        
        public static string Base64Encode(byte[] Data)
        {
            return Convert.ToBase64String(Data);
        }

        public static byte[] Base64Decode(string Data)
        {
            return Convert.FromBase64String(Data);
        }
    }
}
