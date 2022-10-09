namespace roommate_app.Controllers.Login;

public class LoginResponse
{
    public LoginResponse(bool isSuccess, string message)
    {
        IsSuccess = (Boolean)isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; }
    public string Message { get; }
}