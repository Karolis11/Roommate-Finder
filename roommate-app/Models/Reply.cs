using System.ComponentModel.DataAnnotations.Schema;

namespace roommate_app.Models;
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
