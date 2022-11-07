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
using Microsoft.EntityFrameworkCore;

namespace roommate_app.Services;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    User GetById(int id);
    Task<List<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(int id, User user);
    Task DeleteAsync(int id);
}

public class UserService : IUserService
{
    string EncodingString = "aspnet - roommate_app - F4B64644 - 62C9 - 4EB2 - 8F1A - 3D1849E382F2"; // TO CHANGE IN FUTURE

    List<User> _users;

    //private readonly AppSettings _appSettings;
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        //_appSettings = appSettings.Value;
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

    private string generateJwtToken(User user)
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
    public async Task<List<User>> GetAllAsync()
    {
        var existingUsers = await _context.Users.ToListAsync();
        return existingUsers;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var _user = _context.Users.FirstOrDefault(x => x.Id == id);
        _context.Users.Remove(_user);
        await _context.SaveChangesAsync();
    }
}