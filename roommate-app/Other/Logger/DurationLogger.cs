using roommate_app.Other.FileCreator;
using System.Diagnostics.CodeAnalysis;

namespace roommate_app.Other.Logger;
public interface IDurationLogger
{
    void Log(string msg);
    void Message(string msg);
}
[ExcludeFromCodeCoverage]
public class DurationLogger : IDurationLogger
{
    private readonly IFileCreator _file;
    private string path = "log.txt";
    public DurationLogger(IFileCreator file)
    {
        _file = file;
    }

    public void Log(string msg)
    {
        _file.Write(path, "Date: " + DateTime.UtcNow.ToString() + " Message: " + msg, false);
    }

    public void Message(string msg)
    {
        Console.WriteLine(msg);
    }
}