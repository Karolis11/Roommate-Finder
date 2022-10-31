using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using roommate_app.Models;
using roommate_app.Data;
using roommate_app.HelperMethods;

namespace roommate_app.Controllers.Registration;

[Route("[controller]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    private ListingService _listingService;
    public RegistrationController(ApplicationDbContext context)
    {
        _context = context;
        _listingService = new ListingService(context);
    }

    [HttpPost]
    public OkObjectResult Submit([FromBody] User user)
    {
        var emailExistsFlag = false;

        List<User> existingUsers = _context.Users.ToList();

        emailExistsFlag = (
                from User usr in existingUsers
                where usr.Email.Trim().ToLower() == user.Email.Trim().ToLower()
                select usr
            ).Count() > 0;

        if (!emailExistsFlag)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            _listingService.UpdateListings();
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

