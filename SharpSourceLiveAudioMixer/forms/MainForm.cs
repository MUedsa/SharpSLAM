using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using NReco.VideoConverter;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

namespace SharpSourceLiveAudioMixer.forms
{
    public partial class MainForm : Form
    {
        public static MainForm instance = null;
        public static List<SourceGame> Games = new List<SourceGame>
        {
            new SourceGame()
            {
                Id = 730,
                Name = "Counter-Strike: Global Offensive",
                ExeName = "csgo.exe",
                Directory = "Counter-Strike Global Offensive",
                EngineDirectory = "csgo",
                SampleRate = 22050
            },
            new SourceGame()
            {
                Name = "Counter-Strike: Source",
                Directory = "Counter-Strike Source",
                EngineDirectory = "css"
            },
            new SourceGame()
            {
                Name = "Team Fortress 2",
                Directory = "Team Fortress 2",
                EngineDirectory = "tf2",
                SampleRate = 22050
            },
            new SourceGame()
            {
                Name = "Garry's Mod",
                Directory = "GarrysMod",
                EngineDirectory = "garrysmod"
            },
            new SourceGame()
            {
                Name = "Half-Life 2 Deathmatch",
                Directory = "half-life 2 deathmatch",
                EngineDirectory = "hl2mp"
            },
            new SourceGame()
            {
                Name = "Left 4 Dead",
                ExeName = "left4dead.exe",
                Directory = "Left 4 Dead",
                EngineDirectory = "left4dead"
            },
            new SourceGame()
            {
                Name = "Left 4 Dead 2",
                ExeName = "left4dead2.exe",
                Directory = "Left 4 Dead 2",
                EngineDirectory = "left4dead2"
            },
            new SourceGame()
            {
                Name = "Day of Defeat Source",
                Directory = "day of defeat source",
                EngineDirectory = "dod"
            }
        };
        public static BinaryFormatter formatter = new BinaryFormatter();

        public bool Running = false;
        public SourceGame CurrentGame = null;

        public MainForm()
        {
            instance = this;
            new FFMpegConverter().ExtractFFmpeg();
            InitializeComponent();
        }

        #region Game Scanning

        public void scanGames()
        {
            string steamapps = Path.Combine(Program.Config["SteamFolder"],"steamapps");
            comboBox1.Items.Clear();
            try
            {
                var commonFolders = new List<string>();
                validateLibraryFolder(steamapps,commonFolders);
                var matches = new Regex("\"(\\d)\"\\s+\"(.*)\"").Matches(File.ReadAllText(Path.Combine(steamapps,"libraryfolders.vdf")));
                foreach(Match path in matches)
                {
                    validateLibraryFolder(path.Groups[2].Value.Replace("\\\\","\\"),commonFolders);
                }
                foreach(var game in Games)
                {
                    game.InstallDirectory = null;
                    foreach(var path in commonFolders)
                    {
                        if(File.Exists(Path.Combine(path,game.Directory,game.ExeName)))
                        {
                            if(game.InstallDirectory != null)
                            {
                                MessageBox.Show("Duplicate game installation detected.\nGame:" + game.Name + "\nDetected installation in " + game.InstallDirectory + ",another installation detected in " + path,"Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            }
                            else
                            {
                                comboBox1.Items.Add(game.Name);
                            }
                            game.InstallDirectory = path;
                        }
                    }
                }
            }
            catch { }
            comboBox1.Enabled = groupBox_import.Enabled = button_start.Enabled = !(label2.Visible = comboBox1.Items.Count == 0);
            if(comboBox1.Items.Count != 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        public void validateLibraryFolder(string folder,List<string> commonFolders)
        {
            var common = Path.Combine(folder,"common");
            if(Directory.Exists(common))
            {
                commonFolders.Add(common);
            }
            else
            {
                common = Path.Combine(folder,"steamapps","common");
                if(Directory.Exists(common))
                {
                    commonFolders.Add(common);
                }
            }
        }

        #endregion

        #region Media & Game Config Processing

        public string getGameConfigPath(string any)
        {
            string path = Path.GetFullPath("config/" + CurrentGame.Directory);
            Directory.CreateDirectory(path);
            return Path.GetFullPath(Path.Combine(path,Path.GetFileNameWithoutExtension(any)));
        }

        public bool convertNewMedia(string input)
        {
            string path = getGameConfigPath(input) + ".original";
            try
            {
                new FFMpegConverter().Invoke("-i \"" + Path.GetFullPath(input) + "\" -f wav -flags bitexact -map_metadata -1 -vn -acodec pcm_s16le -ar " + CurrentGame.SampleRate + " -ac 1 \"" + path + "\"");
            }
            catch
            {
                return false;
            }
            return File.Exists(path);
        }

        public bool reConvertModifiedMedia(int index)
        {
            // TODO: Add trim and volume adjust
            string path = getGameConfigPath(listView1.Items[index].Text);
            // "-i \"{0}\" -y -f wav -flags bitexact -map_metadata -1 -vn -acodec pcm_s16le -ar {1} -ac 1 {2}-af \"volume={3}\" \"{4}\""
            try
            {
                new FFMpegConverter().Invoke("-i \"" + path + ".original\" -y -f wav -flags bitexact -map_metadata -1 -vn -acodec pcm_s16le -ar " + CurrentGame.SampleRate + " -ac 1 \"" + path + ".modified\"");
            }
            catch
            {
                return false;
            }
            return File.Exists(path);
        }

        public void saveTracks()
        {
            var path = getGameConfigPath("tracks") + ".dat";
            File.Delete(path);
            using(var writer = new StreamWriter(File.OpenWrite(path)))
            {
                foreach(var item in listView1.Items)
                {
                    using(var ms = new MemoryStream())
                    {
                        formatter.Serialize(ms,item);
                        writer.WriteLine(Program.Base64Encode(ms.ToArray()));
                    }
                }
            }
        }

        public void loadTracks()
        {
            listView1.Items.Clear();
            var path = getGameConfigPath("tracks") + ".dat";
            if(File.Exists(path))
            {
                using(var reader = new StreamReader(File.OpenRead(path)))
                {
                    while(!reader.EndOfStream)
                    {
                        var data = reader.ReadLine();
                        if(data != "")
                        {
                            using(var ms = new MemoryStream(Program.Base64Decode(data)))
                            {
                                listView1.Items.Add((ListViewItem)formatter.Deserialize(ms));
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Events

        private void listView1_MouseClick(object sender,MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right && listView1.SelectedItems.Count != 0)
            {
                // TODO: Add menu always crashes my designer
            }
        }

        private void gameRelayWatcher_Changed(object sender,FileSystemEventArgs e)
        {
            // TODO
        }

        private void comboBox1_SelectedIndexChanged(object sender,EventArgs e)
        {
            foreach(var game in Games)
            {
                if(game.Name == comboBox1.Text)
                {
                    CurrentGame = game;
                    loadTracks();
                    return;
                }
            }
            CurrentGame = null;
        }

        private void MainForm_Load(object sender,EventArgs e)
        {
            if(Program.Config["SteamFolder",""] == "")
            {
                try
                {
                    Program.Config["SteamFolder"] = Path.GetDirectoryName(Process.GetProcessesByName("Steam")[0].MainModule.FileName);
                    Program.Config.Save();
                }
                catch
                {
                    MessageBox.Show("Can't detect steam folder automatically,please make sure steam client(steam command isn't supported) is running and restart this program.\nYou can also configure steam folder in settings manually.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            button_playkey.Text = "Play Key: " + Program.Config["PlayKey","BACKSPACE"];
            scanGames();
        }

        private void listView1_MouseDoubleClick(object sender,MouseEventArgs e)
        {
            if(listView1.SelectedItems.Count == 1)
            {
                var item = listView1.SelectedItems[0];
                var dialog = new SetAliasDialog(item.SubItems[1].Text);
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    if(dialog.Result != "")
                    {
                        foreach(ListViewItem check in listView1.Items)
                        {
                            if(check.SubItems[1].Text == dialog.Result)
                            {
                                MessageBox.Show("Alias already exists.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    item.SubItems[1].Text = dialog.Result;
                    saveTracks();
                }
            }
        }

        #endregion

        #region Button Events

        private void button_settings_Click(object sender,EventArgs e)
        {
            new SettingsForm().ShowDialog();
        }

        private void button_import_file_Click(object sender,EventArgs e)
        {
            if(importFileDialog.ShowDialog() == DialogResult.OK)
            {
                var processing = new ProcessingDialog(0,importFileDialog.FileNames.Length);
                new Thread(new ThreadStart(() =>
                {
                    var failed = "";
                    CheckForIllegalCrossThreadCalls = false;
                    foreach(string file in importFileDialog.FileNames)
                    {
                        var key = Path.GetFileNameWithoutExtension(file);
                        if(listView1.Items.ContainsKey(key) && MessageBox.Show("Audio name \"" + Path.GetFileNameWithoutExtension(file) + "\" already exists,continue and override it?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            continue;
                        }
                        listView1.Items.RemoveByKey(key);
                        File.Delete(getGameConfigPath(file) + ".original");
                        if(convertNewMedia(file))
                        {
                            listView1.Items.Add(key,key,0).SubItems.AddRange(new string[]
                            {
                                "","100",listView1.Items.Count.ToString(),""
                            });
                        }
                        else
                        {
                            failed += "\n" + file;
                        }
                        processing.update();
                    }
                    processing.update();
                    saveTracks();
                    MessageBox.Show("Processed " + importFileDialog.FileNames.Length + " file(s)." + (failed == "" ? "" : "\n\nFollowing file(s) convert failed:" + failed),"Done",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    CheckForIllegalCrossThreadCalls = true;
                })).Start();
                processing.ShowDialog();
            }
        }

        private void button_start_Click(object sender,EventArgs e)
        {
            if(Running = !Running)
            {
                button_start.Text = "Stop";
                // TODO

            }
            else
            {
                button_start.Text = "Start";
                // TODO
            }
        }

        private void button_playkey_Click(object sender,EventArgs e)
        {
            var dialog = new SelectKeyDialog(Program.Config["PlayKey"]);
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                Program.Config["PlayKey"] = dialog.Result;
            }
        }

        #endregion

        protected override bool ProcessCmdKey(ref Message msg,Keys keyData)
        {
            if(keyData == Keys.Delete && listView1.SelectedItems.Count != 0)
            {
                foreach(ListViewItem item in listView1.SelectedItems)
                {
                    listView1.Items.Remove(item);
                    var path = getGameConfigPath(item.Text + ".lmao");
                    File.Delete(path + ".original");
                    File.Delete(path + ".modified");
                }
                for(int i = 0;i < listView1.Items.Count;i++)
                {
                    listView1.Items[i].SubItems[3].Text = (i + 1).ToString();
                }
                saveTracks();
            }
            return base.ProcessCmdKey(ref msg,keyData);
        }
    }
}
