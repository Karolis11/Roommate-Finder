using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using roommate_app.Controllers.Authentication;
using roommate_app.Models;
using roommate_app.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace roommate_app.Controllers.Authentication;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    User GetById(int id);
}

public class UserService : IUserService
{

    List<User> _users;

    private readonly AppSettings _appSettings;
    private readonly ApplicationDbContext _context;

    public UserService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
    {
        _appSettings = appSettings.Value;
        _context = context;
        _users = _context.Users.ToList();
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);
        string token = null;
        
        if (user == null)
        {
            user = new User();
            user.Id = -1;
            return new AuthenticateResponse(false, user, token);
        }
        // authentication successful so generate jwt token
        token = generateJwtToken(user);

        return new AuthenticateResponse(true, user, token);
    }

    public User GetById(int id)
    {
        return _users.FirstOrDefault(x => x.Id == id);
    }

    // helper methods
    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("aspnet - roommate_app - F4B64644 - 62C9 - 4EB2 - 8F1A - 3D1849E382F2");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}