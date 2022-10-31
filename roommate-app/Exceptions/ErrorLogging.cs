

public class ErrorLogging {
    private readonly IFileCreator _file;
    private string path = "log.txt";
    public ErrorLogging(IFileCreator file)
    {
        _file = file;
    }

    public void logError(string msg){
        _file.Write(path, "Date: " + DateTime.Now.ToString() + " Message: " + msg, false);
    }

    public void messageError(string msg){
        Console.WriteLine(msg);
    }
}
