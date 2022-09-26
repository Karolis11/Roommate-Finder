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
        bool emailExist = false;

        foreach (var usr in users){
            if(usr.email.ToLower() == user.email.ToLower()) { emailExist = true; }
            if (usr.email.ToLower() == user.email.ToLower() &&
                usr.password == user.password)
            {
                passwordAndEmailCorrect = true;
                break;
            }
        }

        if(!emailExist){
            return JsonSerializer.Serialize(
            new LoginResponse(
                emailExist,
                "The " + user.email + " is not registered"
            )
            );
        }
        else if (emailExist && !passwordAndEmailCorrect){
            return JsonSerializer.Serialize(
            new LoginResponse(
                passwordAndEmailCorrect,
                "Your entered password is incorrect"
            )
            );
        }
        else if(passwordAndEmailCorrect){
            return JsonSerializer.Serialize(
            new LoginResponse(
                passwordAndEmailCorrect,
                "Logged in successfully"
            )
            );
        }
        else{
            return JsonSerializer.Serialize(
            new LoginResponse(
                false,
                "Error, contact support"
            )
            );
        }
        
    }
    
    private List<User> LoadUsers()
    {
        using StreamReader r = new StreamReader("./Data/users.json");
        string json = r.ReadToEnd();
        return JsonSerializer.Deserialize<List<User>>(json);
    }

}
