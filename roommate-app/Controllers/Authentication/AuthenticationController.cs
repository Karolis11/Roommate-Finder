using Microsoft.AspNetCore.Mvc;
using roommate_app.Services;
using System.Diagnostics.CodeAnalysis;

namespace roommate_app.Controllers.Authentication;

[ApiController]
[Route("[controller]")]
[ExcludeFromCodeCoverage]
public class AuthenticationController : ControllerBase
{
    private IUserService _userService;

    public AuthenticationController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

}
