using Microsoft.AspNetCore.Mvc;
using roommate_app.Controllers.Authentication;
using roommate_app.Models;
using roommate_app.Other.ListingComparers;
using roommate_app.Services;

namespace roommate_app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IGenericService _genericService;
        private readonly IUserService _userService;
        public UserController(IGenericService genericService, IUserService userService)
        {
            _genericService = genericService;
            _userService = userService;
        }

        [HttpGet]
        [Route("token")]
        public async Task<JsonResult> GetUserInfo(string token)
        {
            var existingUsers = await _genericService.GetAllAsync<User>();

            var userId = _userService.GetValidatedId(token);

            var user = existingUsers.FirstOrDefault(u => u.Id == 0 || u.Id == 1);

            var userResponse = new UserResponse(user);

            var response = new JsonResult(userResponse);
            response.StatusCode = 200;

            return response;
        }
    }
}
