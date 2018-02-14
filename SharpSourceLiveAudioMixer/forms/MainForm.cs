using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;

using NReco.VideoConverter;

using SharpSourceLiveAudioMixer.dialogs;

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
                NoFadeOut=true,
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
                NoFadeOut=true,
                EngineDirectory = "left4dead2"
            },
            new SourceGame()
            {
                Name = "Day of Defeat Source",
                Directory = "day of defeat source",
                EngineDirectory = "dod"
            },
            new SourceGame()
            {
                Name = "Insurgency",
                ExeName = "insurgency.exe",
                Directory = "insurgency2",
                EngineDirectory = "insurgency"
            }
        };
        public static BinaryFormatter formatter = new BinaryFormatter();

        public int Track = -1;
        public bool Running = false;
        public SourceGame CurrentGame = null;
        public List<FileSystemWatcher> Watchers = new List<FileSystemWatcher>();

        private MenuItem menu_play = null, menu_edit = null, menu_alias = null, menu_trim = null, menu_volume = null, menu_remove = null;

        public MainForm()
        {
            instance = this;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            #region Right Click Menu

            var menu = new ContextMenu();
            menu_play = menu.MenuItems.Add("Play",(sender,e) =>
            {
                var path = getGameConfigPath(listView1.SelectedItems[0].Text,false);
                MusicPlayForm.getInstance(path + (File.Exists(path + ".modified") ? ".modified" : ".original")).Show();
                MusicPlayForm.player.Play();
            });
            menu_edit = menu.MenuItems.Add("Edit",new MenuItem[]
            {
                menu_trim = new MenuItem("Trim",(sender,e) =>
                {
                    var dialog=new TrimDialog();
                    if(dialog.ShowDialog()==DialogResult.OK)
                    {
                        // TODO.
                    }
                }),
                menu_volume = new MenuItem("Adjust Volume",(sender,e) =>
                {
                    var dialog=new AdjustVolumeDialog();
                    if(dialog.ShowDialog()==DialogResult.OK)
                    {
                        var items = listView1.SelectedItems;
                        var processing = new ProcessingDialog(0,items.Count);
                        new Thread(new ThreadStart(() =>
                        {
                            int result=dialog.Result;
                            var failed = "";
                            foreach(ListViewItem item in items)
                            {
                                item.SubItems[2].Text=result.ToString();
                                if(!reConvertModifiedMedia(item))
                                {
                                    failed += "\n" + item.Text;
                                    item.SubItems[2].Text="100";
                                    File.Delete(getGameConfigPath(item.Text,false) + ".modified");
                                }
                                processing.update();
                            }
                            processing.update();
                            saveTracks();
                            MessageBox.Show("Processed " + items.Count + " file(s)." + (failed == "" ? "" : "\n\nFollowing file(s) process failed:" + failed),"Done",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        })).Start();
                        processing.ShowDialog();
                    }
                })
            });
            menu_alias = menu.MenuItems.Add("Set Alias",(sender,e) =>
            {
                listView1_MouseDoubleClick();
            });
            menu_remove = menu.MenuItems.Add("Remove",(sender,e) =>
            {
                if(!Running && listView1.SelectedItems.Count != 0)
                {
                    foreach(ListViewItem item in listView1.SelectedItems)
                    {
                        listView1.Items.Remove(item);
                        var path = getGameConfigPath(item.Text,false);
                        File.Delete(path + ".original");
                        File.Delete(path + ".modified");
                    }
                    for(int i = 0;i < listView1.Items.Count;i++)
                    {
                        listView1.Items[i].SubItems[3].Text = (i + 1).ToString();
                    }
                    saveTracks();
                }
            });
            menu.Popup += (sender,e) =>
            {
                if(listView1.SelectedItems.Count == 0)
                {
                    menu_play.Visible = menu_alias.Visible = menu_edit.Visible = menu_remove.Visible = false;
                }
                else
                {
                    menu_edit.Visible = menu_remove.Visible = true;
                    menu_play.Visible = menu_alias.Visible = menu_trim.Visible = listView1.SelectedItems.Count == 1;
                }
            };
            listView1.ContextMenu = menu;

            #endregion
        }

        private void setEnabled(bool enabled)
        {
            groupBox_import.Enabled = comboBox1.Enabled = button_playkey.Enabled = button_settings.Enabled = enabled;
        }

        protected override bool ProcessCmdKey(ref Message msg,Keys keyData)
        {
            if(keyData == Keys.Delete)
            {
                menu_remove.PerformClick();
            }
            return base.ProcessCmdKey(ref msg,keyData);
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
                                MessageBox.Show("Duplicate game installation detected.\nGame:" + game.Name + "\nDetected installation in " + Path.GetDirectoryName(game.InstallDirectory) + ",another installation detected in " + path,"Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            }
                            else
                            {
                                comboBox1.Items.Add(game.Name);
                            }
                            game.InstallDirectory = Path.Combine(path,game.Directory);
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

        public string getGameConfigPath(string any,bool removeExt = true)
        {
            string path = Path.GetFullPath("config/" + CurrentGame.Directory);
            Directory.CreateDirectory(path);
            return Path.GetFullPath(Path.Combine(path,removeExt ? Path.GetFileNameWithoutExtension(any) : Path.GetFileName(any)));
        }

        public bool convertNewMedia(string input)
        {
            string path = getGameConfigPath(input) + ".original";
            try
            {
                new FFMpegConverter().Invoke("-i \"" + Path.GetFullPath(input) + "\" -f wav -flags bitexact -map_metadata -1 -vn -b:a 192k -acodec pcm_s16le -ar " + CurrentGame.SampleRate + " -ac 1 \"" + path + "\"");
            }
            catch
            {
                return false;
            }
            return File.Exists(path);
        }

        public bool reConvertModifiedMedia(ListViewItem item)
        {
            // TODO: Add trim
            string path = getGameConfigPath(item.Text,false);
            try
            {
                File.Delete(path + ".modified");
                new FFMpegConverter().Invoke("-i \"" + path + ".original\" -y -f wav -flags bitexact -map_metadata -1 -vn -acodec pcm_s16le -ar " + CurrentGame.SampleRate + " -ac 1 -af \"volume=" + (int.Parse(item.SubItems[2].Text) / 100f) + "\" \"" + path + ".modified\"");
            }
            catch
            {
                return false;
            }
            return File.Exists(path + ".modified");
        }

        public void switchTrack(int index)
        {
            if(Running && index < listView1.Items.Count && index != Track)
            {
                if(Track != -1)
                {
                    listView1.Items[Track].SubItems[5] = new ListViewItem.ListViewSubItem();
                }
                listView1.Items[index].SubItems[5] = new ListViewItem.ListViewSubItem(listView1.Items[index],"X");
                Track = index;
                var path = getGameConfigPath(listView1.Items[index].Text,false);
                path += File.Exists(path + ".modified") ? ".modified" : ".original";
                File.Copy(path,Path.Combine(CurrentGame.InstallDirectory,"voice_input.wav"),true);
                File.WriteAllText(Path.Combine(CurrentGame.FullConfigDirectory,"slam_current_track.cfg"),"echo \"[SLAM] Current Track: " + listView1.Items[index].Text + "\"");
            }
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
                                listView1.Items.Add((ListViewItem)formatter.Deserialize(ms)).SubItems[5] = new ListViewItem.ListViewSubItem();
                            }
                        }
                    }
                }
            }
        }

        public void addWatcher(string path)
        {
            var watcher = new FileSystemWatcher
            {
                Path = path,
                Filter = "slam_proxy.cfg",
                NotifyFilter = NotifyFilters.LastWrite,
                EnableRaisingEvents = true,
                IncludeSubdirectories = false
            };
            watcher.Changed += gameConfigProxyChanged;
            Watchers.Add(watcher);
        }

        public void writeGameConfig()
        {
            // slam.cfg
            var slam_config = new StringBuilder()
                .AppendLine("alias slam_list \"exec slam_tracks\"")
                .AppendLine("alias la slam_list")
                .AppendLine("alias list slam_list")
                .AppendLine("alias tracks slam_list")
                .AppendLine()
                .AppendLine("alias slam_toggle slam_on")
                .AppendLine("alias slam_on \"alias slam_toggle slam_off;voice_inputfromfile 1;voice_loopback 1;+voicerecord\"")
                .AppendLine("alias slam_off \"alias slam_toggle slam_on;-voicerecord;voice_inputfromfile 0;voice_loopback 0;\"")
                .AppendLine("alias slam_sync \"host_writeconfig slam_proxy\"")
                .AppendLine("alias slam_current \"exec slam_current_track\"");
            if(Program.Config.GetBool("HoldToPlay",false))
            {
                slam_config.AppendLine("alias +slam_hold_play slam_on")
                    .AppendLine("alias -slam_hold_play slam_off")
                    .AppendLine("bind " + Program.Config["PlayKey"] + " +slam_hold_play");
            }
            else
            {
                slam_config.AppendLine("bind " + Program.Config["PlayKey"] + " slam_toggle");
            }
            slam_config.AppendLine()
                .AppendLine("voice_enable 1")
                .AppendLine("voice_modenable 1")
                .AppendLine("voice_forcemicrecord 0");
            if(!CurrentGame.NoFadeOut)
            {
                slam_config.AppendLine("voice_fadeouttime 0");
            }
            slam_config.AppendLine();
            for(int i = 0;i < listView1.Items.Count;i++)
            {
                var item = listView1.Items[i];
                var cmd = " \"bind " + Program.Config["ProxyKey","F1"] + " " + i + ";slam_sync;slam_off;echo Loaded: " + item.Text + "\"";
                slam_config.AppendLine("alias " + (i + 1) + cmd);
                if(item.SubItems[1].Text != "")
                {
                    slam_config.AppendLine("alias \"" + item.SubItems[1].Text + "\"" + cmd);
                }
            }
            slam_config.AppendLine().AppendLine("echo \"SLAM has been loaded.Type la for tracks list or just press play key in game.\"");
            File.WriteAllText(Path.Combine(CurrentGame.FullConfigDirectory,"slam.cfg"),slam_config.ToString());

            // slam_tracks.cfg
            var slam_tracks = new StringBuilder()
                .AppendLine("echo \"You can select tracks by typing alias or track number.\"")
                .AppendLine("echo \"------------------SLAM Tracks-----------------\"");
            for(int i = 0;i < listView1.Items.Count;i++)
            {
                var item = listView1.Items[i];
                slam_tracks.AppendLine("echo \"" + (i + 1) + ". " + item.Text + (item.SubItems[1].Text == "" ? "" : " [" + item.SubItems[1].Text + "]\""));
            }
            slam_tracks.AppendLine("echo \"----------------------------------------------\"");
            File.WriteAllText(Path.Combine(CurrentGame.FullConfigDirectory,"slam_tracks.cfg"),slam_tracks.ToString());
        }

        public void gameConfigProxyChanged(object sender,FileSystemEventArgs e)
        {
            if(Running && CurrentGame != null && File.Exists(e.FullPath))
            {
                try
                {
                    var match = new Regex("bind \"?" + Program.Config["ProxyKey"] + "\"? \"?(\\d)\"?",RegexOptions.IgnoreCase).Match(File.ReadAllText(e.FullPath));
                    if(match.Success && int.TryParse(match.Groups[1].Value,out int index))
                    {
                        switchTrack(index);
                    }
                    File.Delete(e.FullPath);
                }
                catch
                {
                    Thread.Sleep(10);
                    gameConfigProxyChanged(sender,e);
                }
            }
        }

        #endregion

        #region Other Events

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
            new FFMpegConverter().ExtractFFmpeg();
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

        private void MainForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            if(button_start.Text == "Stop")
            {
                button_start.PerformClick();
            }
        }

        private void listView1_MouseDoubleClick(object sender = null,MouseEventArgs e = null)
        {
            if(!Running && listView1.SelectedItems.Count == 1)
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
                button_import_file.Enabled = false;
                var files = importFileDialog.FileNames;
                var processing = new ProcessingDialog(0,files.Length);
                new Thread(new ThreadStart(() =>
                {
                    var failed = "";
                    foreach(string file in files)
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
                                "","100",listView1.Items.Count.ToString(),"",""
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
                    button_import_file.Enabled = true;
                    MessageBox.Show("Processed " + files.Length + " file(s)." + (failed == "" ? "" : "\n\nFollowing file(s) convert failed:" + failed),"Done",MessageBoxButtons.OK,MessageBoxIcon.Information);
                })).Start();
                processing.ShowDialog();
            }
        }

        private void button_start_Click(object sender,EventArgs e)
        {
            if(CurrentGame == null)
            {
                return;
            }
            if(listView1.Items.Count == 0)
            {
                MessageBox.Show("Please import media files before start.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            if(Running = !Running)
            {
                button_start.Text = "Stop";
                if(CurrentGame.Id == -1)
                {
                    addWatcher(CurrentGame.FullConfigDirectory);
                }
                else
                {
                    var sub = Path.Combine(CurrentGame.Id.ToString(),"local/cfg");
                    foreach(var dir in Directory.EnumerateDirectories(Path.Combine(Program.Config["SteamFolder"],"userdata"),"*",SearchOption.TopDirectoryOnly))
                    {
                        addWatcher(Path.Combine(dir,sub));
                    }
                }
                writeGameConfig();
                switchTrack(0);
            }
            else
            {
                foreach(var watcher in Watchers)
                {
                    watcher.Dispose();
                }
                Watchers.Clear();
                foreach(ListViewItem item in listView1.Items)
                {
                    item.SubItems[5] = new ListViewItem.ListViewSubItem();
                }
                Track = -1;
                button_start.Text = "Start";
                File.Delete(Path.Combine(CurrentGame.InstallDirectory,"voice_input.wav"));
                File.Delete(Path.Combine(CurrentGame.FullConfigDirectory,"slam.cfg"));
                File.Delete(Path.Combine(CurrentGame.FullConfigDirectory,"slam_tracks.cfg"));
                File.Delete(Path.Combine(CurrentGame.FullConfigDirectory,"slam_current_track.cfg"));
            }
            setEnabled(!Running);
        }

        private void button_playkey_Click(object sender,EventArgs e)
        {
            var dialog = new SelectKeyDialog(Program.Config["PlayKey"]);
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                Program.Config["PlayKey"] = dialog.Result;
                Program.Config.Save();
            }
        }

        private void button_import_youtube_Click(object sender,EventArgs e)
        {

        }

        private void button_import_netease_Click(object sender,EventArgs e)
        {

        }

        #endregion
    }
}
