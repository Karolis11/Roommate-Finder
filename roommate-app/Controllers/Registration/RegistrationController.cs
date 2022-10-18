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
    private readonly IFileCreator _file;

    public RegistrationController(IFileCreator file)
    {
        _file = file;
    }

    [HttpPost]
    public OkObjectResult Submit([FromBody] User user)
    {
        var emailExistsFlag = false;

        List<User> existingUsers = LoadUsers();

        emailExistsFlag = (
                from User usr in existingUsers
                where usr.Email.Trim().ToLower() == user.Email.Trim().ToLower()
                select usr
            ).Count() > 0;

        if (!emailExistsFlag)
        {
            // if email does not exist, add the user
            existingUsers.Add(user);
            string json = JsonSerializer.Serialize(existingUsers);
            _file.Write("Data/users.json", json, false);
        }

        return base.Ok(
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
        string json = _file.ReadToEndFile("./Data/users.json");
        return JsonSerializer.Deserialize<List<User>>(json);
    }
}

