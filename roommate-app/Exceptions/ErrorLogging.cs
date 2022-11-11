using roommate_app.Other.FileCreator;

namespace roommate_app.Exceptions;
public interface IErrorLogging
{
    public void logError(string msg);
    public void messageError(string msg);
}
public class ErrorLogging : IErrorLogging
{
    private readonly IFileCreator _file;
    private string path = "log.txt";
    public ErrorLogging(IFileCreator file)
    {
        _file = file;
    }

    public void logError(string msg){
        _file.Write(path, "Date: " + DateTime.UtcNow.ToString() + " Message: " + msg, false);
    }

    public void messageError(string msg)
    {
        Console.WriteLine(msg);
    }
}
