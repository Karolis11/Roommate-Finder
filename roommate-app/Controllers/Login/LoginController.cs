using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using roommate_app.Models;
using System.Text.Json;
using System.IO;
using roommate_app.Other.FileCreator;
using roommate_app.Exceptions;
using roommate_app.Services;

namespace roommate_app.Controllers.Login;

[Route("[controller]")]
[ApiController]

public class LoginController : ControllerBase{

    private readonly IErrorLogging _errorLogging;
    private readonly IUserService _userService;
    public LoginController(ErrorLogging errorLogging, IUserService userService)
    {
        _errorLogging = errorLogging;
        _userService = userService;
    }

    [HttpPost]
    public async Task<OkObjectResult> Submit([FromBody] User user){

        bool passwordAndEmailCorrect = false;

        try{
            List<User> existingUsers = await _userService.GetAllAsync();
            passwordAndEmailCorrect = (
                    from User usr in existingUsers
                    where usr.Email.ToLower() == user.Email.ToLower()
                        && usr.Password == user.Password
                    select usr
                ).Count() == 1;
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
    
}
