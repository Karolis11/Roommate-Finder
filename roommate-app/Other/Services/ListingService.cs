using Microsoft.EntityFrameworkCore;
using roommate_app.Data;
using roommate_app.Models;
using roommate_app.Other.WebSocket;

namespace roommate_app.Services;

public interface IListingService
{
    IList<Listing> GetByUserId(int id);
    Task UpdateAsync(int id, Listing listing);
}

public class ListingService : IListingService
{
    private Lazy<List<Listing>> _listings => new Lazy<List<Listing>>(() => _context.Listings.ToList());
    private readonly ApplicationDbContext _context;

    public delegate void ListingUpdatedEventHandler(object source, EventArgs e);
    public event ListingUpdatedEventHandler ListingUpdated;

    public ListingService(ApplicationDbContext context)
    {
        _context = context;
        ListingUpdated += PusherChannel.OnListingUpdated;
    }

    public IList<Listing> GetByUserId(int id)
    {
        IList<Listing> _userListings = new List<Listing>();
        IList<Listing> _existingListings = new List<Listing>((IEnumerable<Listing>)_listings);
        for (int i = 0; i < _existingListings.Count; i++)
        {
            if (_existingListings[i].UserId == id)
            {
                _userListings.Add(_existingListings[i]);
            }
        }
        return _userListings;
    }
    public async Task UpdateAsync(int id, Listing listing)
    {
        var lst = _context.Listings.Where(l => l.Id == id).First();
        lst.Phone = listing.Phone;
        lst.RoommateCount = listing.RoommateCount;
        lst.MaxPrice = listing.MaxPrice;
        lst.ExtraComment = listing.ExtraComment;
        await _context.SaveChangesAsync();
        OnListingUpdated();
    }
    protected virtual void OnListingUpdated()
    {
        if (ListingUpdated != null)
        {
            ListingUpdated(this, EventArgs.Empty);
        }
    }

}