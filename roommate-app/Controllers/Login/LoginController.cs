using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using roommate_app.Models;
using System.Text.Json;
using System.IO;
using roommate_app.Other.FileCreator;
using roommate_app.Exceptions;

namespace roommate_app.Controllers.Login;

[Route("[controller]")]
[ApiController]

public class LoginController : ControllerBase{

    private readonly IFileCreator _file;
    ErrorLogging _errorLogging;

    public LoginController(ErrorLogging errorLogging)
    {
        _errorLogging = _errorLogging;
    }

    [HttpPost]
    public OkObjectResult Submit([FromBody] User user){

        
        bool passwordAndEmailCorrect = false;

        try{
            List<User> users = LoadUsers();
            passwordAndEmailCorrect = (
                    from User usr in users
                    where usr.Email.ToLower() == user.Email.ToLower()
                        && usr.Password == user.Password
                    select usr
                ).Count() == 1;
        }
        catch(FileNotFoundException e){
            _errorLogging.logError(e.Message);
            _errorLogging.messageError("The listings were not found");
        }
        catch(InvalidOperationException e){
            _errorLogging.logError(e.Message);
            _errorLogging.messageError("Linq expression failed");
        }
        catch(Exception e){
            _errorLogging.logError(e.Message);
            _errorLogging.messageError("Unexpected error, please restart the program");
        }

        return base.Ok(
            new LoginResponse(
                passwordAndEmailCorrect,
                passwordAndEmailCorrect
                    ? "Logged in successfully."
                    : "Either email or password is incorrect.",
                passwordAndEmailCorrect
                    ? user.Email
                    : string.Empty,
                passwordAndEmailCorrect
                    ? "asd"
                    : string.Empty
            )
        );
    }
    
    private List<User> LoadUsers()
    {
        string json = _file.ReadToEndFile("./Data/users.json");
        Console.WriteLine(json);
        return JsonSerializer.Deserialize<List<User>>(json);
    }

}
