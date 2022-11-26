using Microsoft.AspNetCore.Mvc;
using roommate_app.Models;
using roommate_app.Services;
using System.Diagnostics.CodeAnalysis;

namespace roommate_app.Controllers.Registration;

[Route("[controller]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly IGenericService _genericService;
    public RegistrationController(IGenericService genericService)
    {
        _genericService = genericService;
    }

    [HttpPost]
    public async Task<JsonResult> Submit([FromBody] User user)
    {
        var emailExistsFlag = false;
        var response = new JsonResult(new Object());

        List<User> existingUsers = await _genericService.GetAllAsync<User>();

        emailExistsFlag = (
                from User usr in existingUsers
                where usr.Email.Trim().ToLower() == user.Email.Trim().ToLower()
                select usr
            ).Count() > 0;

        if (!emailExistsFlag)
        {
            await _genericService.AddAsync<User>(user);
        }

        if (!emailExistsFlag)
        {
            response = new JsonResult(new RegistrationResponse(
                true,
                "Your account has been successfully created."
            ));
            response.StatusCode = 201;
        } else
        {
            response = new JsonResult(new RegistrationResponse(
                false,
                "Account with this email already exists"
            ));
            response.StatusCode = 200;
        }

        return response;
    }
}

