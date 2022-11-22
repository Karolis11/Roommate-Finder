using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using roommate_app.Controllers.Authentication;
using roommate_app.Data;
using roommate_app.Models;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace roommate_app.Services;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    User GetById(int id);
}

public class UserService : IUserService
{
    string EncodingString = "aspnet - roommate_app - F4B64644 - 62C9 - 4EB2 - 8F1A - 3D1849E382F2"; // TO CHANGE IN FUTURE

    List<User> _users;

    // private readonly AppSettings _appSettings;
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        // _appSettings = appSettings.Value;
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
        token = GenerateJwtToken(user);

        return new AuthenticateResponse(true, user, token);
    }

    private string GenerateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(EncodingString);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public User GetById(int id)
    {
        return _users.FirstOrDefault(x => x.Id == id);
    }
}