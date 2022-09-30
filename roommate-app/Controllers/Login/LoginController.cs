using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using roommate_app.Models;
using System.Text.Json;
using System.IO;

namespace roommate_app.Controllers.Login;

[Route("[controller]")]
[ApiController]

public class LoginController : ControllerBase{

    [HttpPost]
    public string Submit([FromBody] User user){

        List<User> users = LoadUsers();
        bool passwordAndEmailCorrect = false;

        passwordAndEmailCorrect = (
                from User usr in users
                where usr.Email.ToLower() == user.Email.ToLower()
                      && usr.Password == user.Password
                select usr
            ).Count() == 1;

        return JsonSerializer.Serialize(
            new LoginResponse(
                passwordAndEmailCorrect,
                passwordAndEmailCorrect
                    ? "Logged in successfully."
                    : "Either email or password is incorrect."
            )
        ); 
    }
    
    private List<User> LoadUsers()
    {
        using StreamReader r = new StreamReader("./Data/users.json");
        string json = r.ReadToEnd();
        return JsonSerializer.Deserialize<List<User>>(json);
    }

}
