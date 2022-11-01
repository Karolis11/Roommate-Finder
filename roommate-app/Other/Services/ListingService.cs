using System.ComponentModel;
using System.Runtime.CompilerServices;
using roommate_app.Models;
using roommate_app.Data;
using Microsoft.EntityFrameworkCore;

namespace roommate_app.Services;

public interface IListingService
{
    IEnumerable<Listing> GetByUserId(int id);
    Task UpdateListings();
    Task<IEnumerable<Listing>> GetAll();
    void Add(Listing listing);
    void Update(int id, Listing listing);
    void Delete(int id);
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
    public async Task UpdateListings()
    {
        _listings = await _context.Listings.ToListAsync();
    }
    public void Add(Listing listing)
    {
        throw new NotImplementedException();
    }
    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
    public Task<IEnumerable<Listing>> GetAll()
    {
        throw new NotImplementedException();
    }
    public void Update(int id, Listing listing)
    {
        throw new NotImplementedException();
    }

    


}