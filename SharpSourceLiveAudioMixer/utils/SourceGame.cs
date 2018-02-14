using System.IO;

public class SourceGame
{
    public bool NoFadeOut = false;
    public int Id = -1, SampleRate = 11025;
    public string Name, Directory, ConfigDirectory = "cfg", EngineDirectory = "hl2", FullConfigDirectory, ExeName = "hl2.exe";

    private string _installDirectory;
    public string InstallDirectory
    {
        get => _installDirectory;
        set
        {
            _installDirectory = value;
            FullConfigDirectory = value == null ? null : Path.Combine(_installDirectory,EngineDirectory,ConfigDirectory);
        }
    }
}
