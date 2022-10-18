namespace roommate_app.Controllers.Login;

public class LoginResponse
{
    public LoginResponse(bool isSuccess, string message, string email, string accessToken)
    {
        IsSuccess = isSuccess;
        Message = message;
        Email = email;
        AccessToken = accessToken;
    }

    public bool IsSuccess { get; }
    public string Message { get; }
    public string Email { get; }
    public string AccessToken { get; }

}