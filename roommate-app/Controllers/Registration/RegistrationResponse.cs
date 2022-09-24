namespace roommate_app.Controllers.Registration;

public class RegistrationResponse
{
    public RegistrationResponse(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public bool Success { get; }
    public string Message { get; }
}
