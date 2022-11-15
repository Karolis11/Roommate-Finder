using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace roommate_app.Controllers.Authentication
{
    [ExcludeFromCodeCoverage]

    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
