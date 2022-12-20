using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace roommate_app.Models;
[ExcludeFromCodeCoverage]
public class Reply
{
    public int Id { get; set; }
    public string Message { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual User User { get; set; }
    [ForeignKey("Listing")]
    public int ListingId { get; set; }
    public virtual Listing Listing { get; set; }
}
