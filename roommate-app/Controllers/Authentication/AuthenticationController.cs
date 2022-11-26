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
    public JsonResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
        {
            var jsonResultUnauthorized = new JsonResult(new { message = "Username or password is incorrect" });
            jsonResultUnauthorized.StatusCode = 401;
            return jsonResultUnauthorized;
        }

        var jsonResult = new JsonResult(response);
        jsonResult.StatusCode = 200;

        return jsonResult;
    }

}
