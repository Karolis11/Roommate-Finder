using roommate_app.Data;
using roommate_app.Models;

namespace roommate_app.Other.Services;

public interface IReplyService
{
    List<Reply> GetByListingId(int listingId);
}
public class ReplyService: IReplyService
{
    private ApplicationDbContext _context;
    public ReplyService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Reply> GetByListingId(int listingId)
    {
        var replies = _context.Replies.Where<Reply>(r => r.ListingId == listingId).ToList();
        return (List<Reply>)replies;
    }
}

