using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using roommate_app.Exceptions;
using roommate_app.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using System.Drawing.Text;
using Org.BouncyCastle.Bcpg;

namespace roommate_app.Controllers.Authentication;
[ExcludeFromCodeCoverage]
public class JwtMiddleware
{
    string EncodingString = "aspnet - roommate_app - F4B64644 - 62C9 - 4EB2 - 8F1A - 3D1849E382F2"; // TO CHANGE IN FUTURE

    private readonly RequestDelegate _next;
    private IErrorLogging _errorLogging;
    
    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context, IUserService userService, IErrorLogging errorLogging)
    {
        _errorLogging = errorLogging;
        var userId = -1;
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null) {
            userId = userService.GetValidatedId(token);
            if (userId != -1) {
                AttachUserToContext(context, userService, userId);
            }
        }
        await _next(context);
    }


    private void AttachUserToContext(HttpContext context, IUserService userService, int userId)
    {
        try 
        { 
            context.Items["User"] = userService.GetById(userId);
        }
        catch (Exception e)
        {
            // do nothing if jwt validation fails
            // user is not attached to context so request won't have access to secure routes
            _errorLogging.LogError(e.Message);
            _errorLogging.MessageError("Authentication failed, please try again");
        }
    }
}