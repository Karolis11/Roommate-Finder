using System.ComponentModel;
using System.Runtime.CompilerServices;
using roommate_app.Data;
using roommate_app.HelperMethods;

namespace roommate_app.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string City { get; set; }
    public Lazy<IList<Listing>> Listings => new Lazy<IList<Listing>>(() => ListingService.GetByUserId(this.Id));

}

