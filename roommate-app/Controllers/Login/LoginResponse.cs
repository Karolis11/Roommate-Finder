namespace roommate_app.Controllers.Login;

public class LoginResponse
{
    public LoginResponse(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public bool Success { get; }
    public string Message { get; }
}