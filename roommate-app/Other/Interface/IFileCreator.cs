public interface IFileCreator
{
    void Write(string path, string stringToWrite, bool append);
    string ReadToEndFile(string path);
}

public class FileCreator : IFileCreator
{
    void IFileCreator.Write(string path, string stringToWrite, bool append)
    {
        using StreamWriter tsw = new StreamWriter(path, append);
        tsw.WriteLine(stringToWrite);
        tsw.Close();
    }

    string IFileCreator.ReadToEndFile(string path)
    {
        using StreamReader r = new StreamReader(path);
        string json = r.ReadToEnd();
        r.Close();
        return json;
    }
}