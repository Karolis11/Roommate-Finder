using Microsoft.EntityFrameworkCore;
using roommate_app.Data;
using roommate_app.Models;

namespace roommate_app.Services;

public interface IListingService
{
    IList<Listing> GetByUserId(int id);
    Task<List<Listing>> GetAllAsync();
    Task AddAsync(Listing listing);
    Task UpdateAsync(int id, Listing listing);
    Task DeleteAsync(int id);
}

public class ListingService : IListingService
{
    private Lazy<List<Listing>> _listings => new Lazy<List<Listing>>(() => _context.Listings.ToList());
    private readonly ApplicationDbContext _context;

    public ListingService(ApplicationDbContext context)
    {
        _context = context;
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
    public async Task<List<Listing>> GetAllAsync()
    {
        var existingListings = await _context.Listings.ToListAsync();
        return existingListings;
    }
    public async Task AddAsync(Listing listing)
    {
        await _context.Listings.AddAsync(listing);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(int id, Listing listing)
    {
        _context.Listings.Update(listing);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var _listing = _context.Listings.FirstOrDefault(x => x.Id == id);
        _context.Listings.Remove(_listing);
        await _context.SaveChangesAsync();
    }

}