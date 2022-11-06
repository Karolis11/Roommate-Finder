using System.ComponentModel;
using System.Runtime.CompilerServices;
using roommate_app.Models;
using roommate_app.Data;
using Microsoft.EntityFrameworkCore;

namespace roommate_app.Services;

public interface IListingService
{
    IEnumerable<Listing> GetByUserId(int id);
    Task<List<Listing>> GetAllAsync();
    Task AddAsync(Listing listing);
    Task UpdateAsync(int id, Listing listing);
    Task DeleteAsync(int id);
}

public class ListingService : IListingService
{
    List<Listing> _listings;

    private readonly ApplicationDbContext _context;

    public ListingService(ApplicationDbContext context)
    {
        _context = context;
        _listings = _context.Listings.ToList();
    }

    public IEnumerable<Listing> GetByUserId(int id)
    {
        List<Listing> _userListings = new List<Listing>();
        for (int i = 0; i < _listings.Count; i++)
        {
            if (_listings[i].UserId == id)
            {
                _userListings.Add(_listings[i]);
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