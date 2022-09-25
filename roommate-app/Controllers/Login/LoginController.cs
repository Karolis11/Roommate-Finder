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

        foreach (var usr in users)
        {
            if (usr.email.ToLower() == user.email.ToLower() &&
                usr.password == user.password)
            {
                passwordAndEmailCorrect = true;
                break;
            }
        }


        return JsonSerializer.Serialize(
            new LoginResponse(
                passwordAndEmailCorrect,
                passwordAndEmailCorrect 
                    ? "Logged in successfully"
                    : "Your email or password is not correct"
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
