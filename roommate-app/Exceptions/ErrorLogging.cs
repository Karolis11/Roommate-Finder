using roommate_app.Other.FileCreator;

namespace roommate_app.Exceptions;
public interface IErrorLogging
{
    void LogError(string msg);
    void MessageError(string msg);
}
public class ErrorLogging : IErrorLogging
{
    private readonly IFileCreator _file;
    private string path = "log.txt";
    public ErrorLogging(IFileCreator file)
    {
        _file = file;
    }

    public void LogError(string msg)
    {
        _file.Write(path, "Date: " + DateTime.UtcNow.ToString() + " Message: " + msg, false);
    }

    public void MessageError(string msg)
    {
        Console.WriteLine(msg);
    }
}
