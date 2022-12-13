using System.Diagnostics.CodeAnalysis;

namespace roommate_app.Controllers.Replies;
[ExcludeFromCodeCoverage]
public class ReplyRequest
{
    public int ListingId { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; }
}
