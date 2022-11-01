using Microsoft.AspNetCore.Mvc;
using roommate_app.Models;
using roommate_app.Data;
using roommate_app.Other.ListingComparers;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using roommate_app.Exceptions;
using roommate_app.Services;

namespace roommate_app.Controllers;

[ApiController]
[Route("[controller]")]
public class ListingController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IListingCompreterFactory _listingFactory;
    private readonly IErrorLogging _errorLogging;
    private readonly ApplicationDbContext _context;

    public ListingController(
        ILogger<HomeController> logger, 
        IListingCompreterFactory listingFactory, 
        ApplicationDbContext context, 
        IErrorLogging errorLogging
        )
    {
        _logger = logger;
        _listingFactory = listingFactory;
        _context = context;
        _errorLogging = errorLogging;
    }

    [HttpGet]
    [Route("sort")]
    public ActionResult GetSortedListings(SortMode sort, string city)
    {
        var existingListings = _context.Listings.ToList();

        var factory = _listingFactory.createListingComparerFactory();
        var comparer = factory.GetComparer(sortMode: sort, city: city);

        existingListings.Sort(comparer);

        return base.Ok(existingListings);
    }

    [HttpPost]
    public ActionResult Submit([FromBody] Listing listing)
    {
        List<Listing> existingListings = new List<Listing>();
        listing.Date = DateTime.Now.ToString("yyyy-MM-dd");

        try{
            _context.Listings.Add(listing);
            _context.SaveChanges();
            existingListings.Add(listing);
        }
        catch(ArgumentNullException e){
            _errorLogging.logError(e.Message);
            _errorLogging.messageError("Failed to load existing listing");
        }
        catch(SqlException e){
            _errorLogging.logError(e.Message);
            _errorLogging.messageError("Could not insert a listing into the database.");
        }
        catch(Exception e){
            _errorLogging.logError(e.Message);
            _errorLogging.messageError("Unexpected error, please restart the program");
        }

        return base.Ok(_context.Listings.ToList());
    }
}
