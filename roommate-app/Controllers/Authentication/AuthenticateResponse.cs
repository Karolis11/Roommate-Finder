using roommate_app.Models;

namespace roommate_app.Controllers.Authentication
{
    public class AuthenticateResponse
    {
        public bool SuccessfulLogin { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(bool b, User user, string token)
        {
            SuccessfulLogin = b;
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            City = user.City;
            Token = token;
        }
    }
}
