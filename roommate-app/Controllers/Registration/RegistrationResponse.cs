namespace roommate_app.Controllers.Registration;

public class RegistrationResponse
{
    public RegistrationResponse(bool isSuccess, string message)
    {
        IsSuccess = (Boolean)isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; }
    public string Message { get; }
}
