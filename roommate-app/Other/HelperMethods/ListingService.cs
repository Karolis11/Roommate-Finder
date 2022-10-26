using System.ComponentModel;
using System.Runtime.CompilerServices;
using roommate_app.Models;
using roommate_app.Data;

namespace roommate_app.HelperMethods;

public class ListingService
{

    static List<Listing> _listings;

    private readonly ApplicationDbContext _context;

    public ListingService(ApplicationDbContext context)
    {
        _context = context;
        _listings = _context.Listings.ToList();
    }



    public static List<Listing> GetByUserId(int id)
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

    public void UpdateListings()
    {
        _listings = _context.Listings.ToList();
    }


}