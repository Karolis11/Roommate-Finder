using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using roommate_app.Models;
using System.Text.Json;
using System.IO;

namespace roommate_app.Controllers.Login;

[Route("[controller]")]
[ApiController]

public class LoginController : ControllerBase{

    private readonly IFileCreator _file;

    public LoginController(IFileCreator file)
    {
        _file = file;
    }

    [HttpPost]
    public OkObjectResult Submit([FromBody] User user){

        List<User> users = LoadUsers();
        bool passwordAndEmailCorrect = false;

        passwordAndEmailCorrect = (
                from User usr in users
                where usr.Email.ToLower() == user.Email.ToLower()
                      && usr.Password == user.Password
                select usr
            ).Count() == 1;

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
