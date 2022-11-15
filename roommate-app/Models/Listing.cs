using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace roommate_app.Models;
[ExcludeFromCodeCoverage]
public class Listing
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }
    public int RoommateCount { get; set; }
    public int MaxPrice { get; set; }
    public string ExtraComment { get; set; }
    public string Date { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public string FullName()
    {
        return $"{FirstName} {LastName}";
    }
}

