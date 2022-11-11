using Microsoft.AspNetCore.Mvc;
using roommate_app.Models;
using roommate_app.Services;

namespace roommate_app.Controllers.Registration;

[Route("[controller]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly IUserService _userService;

    public RegistrationController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<OkObjectResult> Submit([FromBody] User user)
    {
        var emailExistsFlag = false;

        List<User> existingUsers = await _userService.GetAllAsync();

        emailExistsFlag = (
                from User usr in existingUsers
                where usr.Email.Trim().ToLower() == user.Email.Trim().ToLower()
                select usr
            ).Count() > 0;

        if (!emailExistsFlag)
        {
            await _userService.AddAsync(user);
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
}

