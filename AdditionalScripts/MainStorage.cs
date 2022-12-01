using IOSerializer;

namespace MAUIStorage;

public class MainStorage
{
    public enum Game : int
    {
    }

    public string Path { get; } = FileSystem.Current.AppDataDirectory + @"/main.suv";

    private readonly Dictionary<string, float> floats;
    private readonly Dictionary<string, string> strings;

    public Game PrevGame { get { return _prevGame; } set { _prevGame = value; Save(); } }
    private Game _prevGame = 0;

    public void Save()
    {
        FlushToLists();
        FileStream stream = File.OpenWrite(Path);
        Serializer.Serialize(stream, strings, floats);
        stream.Close();
    }

    private void FlushToLists()
    {
        floats[nameof(PrevGame)] = (int)PrevGame;
    }

    public MainStorage()
    {
        if (!File.Exists(Path))
        {
            var file = File.Create(Path);
            file.Close();
            _prevGame = 0;
            return;
        }

        FileStream stream = File.OpenRead(Path);
        Serializer.Deserialize(stream, out strings, out floats);
        stream.Close();

        if (!floats.ContainsKey(nameof(PrevGame)))
            floats.Add(nameof(PrevGame), 0);

        PrevGame = (Game)floats[nameof(PrevGame)];
    }
}