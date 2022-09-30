using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using roommate_app.Models;
using System.Text.Json;
using System.IO;

namespace roommate_app.Controllers.Registration;

[Route("[controller]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    [HttpPost]
    public string Submit([FromBody] User user)
    {
        var emailExistsFlag = false;

        List<User> existingUsers = LoadUsers();

        foreach (var usr in existingUsers)
        {
            // trim and lowercase the strings
            if (usr.Email.Trim().ToLower() 
                == user.Email.Trim().ToLower())
            {
                emailExistsFlag = true; 
                break;
            }
        }

        if (!emailExistsFlag)
        {
            // if email does not exist, add the user
            existingUsers.Add(user);
            string json = JsonSerializer.Serialize(existingUsers);
            using StreamWriter tsw = new StreamWriter("Data/users.json", false);
            tsw.WriteLine(json);
            tsw.Close();
        }

        return JsonSerializer.Serialize(
            new RegistrationResponse(
                !emailExistsFlag,
                emailExistsFlag 
                    ? "Account with this email already exists"
                    : "Your account has been successfully created."
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

