using System.ComponentModel;
using System.Runtime.CompilerServices;
using roommate_app.Models;
using roommate_app.Data;
using Microsoft.EntityFrameworkCore;
using roommate_app.Other.WebSocket;

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

    public delegate void ListingUpdatedEventHandler(object source, EventArgs e);
    public event ListingUpdatedEventHandler ListingUpdated;

    public ListingService(ApplicationDbContext context)
    {
        _context = context;
        _listings = _context.Listings.ToList();
        ListingUpdated += PusherChannel.OnListingUpdated;
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
        var lst = _context.Listings.Where(l => l.Id == id).First();
        lst.Phone = listing.Phone;
        lst.RoommateCount = listing.RoommateCount;
        lst.MaxPrice = listing.MaxPrice;
        lst.ExtraComment = listing.ExtraComment;
        await _context.SaveChangesAsync();
        OnListingUpdated();
    }
    public async Task DeleteAsync(int id)
    {
        var _listing = _context.Listings.FirstOrDefault(x => x.Id == id);
        _context.Listings.Remove(_listing);
        await _context.SaveChangesAsync();
    }

    protected virtual void OnListingUpdated()
    {
        if (ListingUpdated != null)
        {
            ListingUpdated(this, EventArgs.Empty);
        }
    }

}