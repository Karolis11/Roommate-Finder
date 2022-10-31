using System.ComponentModel;
using System.Runtime.CompilerServices;
using roommate_app.Models;
using roommate_app.Data;
using Microsoft.EntityFrameworkCore;

namespace roommate_app.HelperMethods;

public interface IListingService
{
    List<Listing> GetByUserId(int id);
    Task UpdateListings();
}

public class ListingService : IListingService
{
    static List<Listing> _listings;

    private readonly ApplicationDbContext _context;

    public ListingService(ApplicationDbContext context)
    {
        _context = context;
        _listings = _context.Listings.ToList();
    }



    public List<Listing> GetByUserId(int id)
    {
        List<Listing> _userListings = new List<Listing>();
        for(int i = 0; i < _listings.Count; i++)
        {
            if (_listings[i].UserId == id){
                _userListings.Add(_listings[i]);
            }
        }
        return _userListings;
    }

    public async Task UpdateListings()
    {
        _listings = await _context.Listings.ToListAsync();
    }


}