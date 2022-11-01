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
    private readonly IListingService _listingService;

    public ListingController(
        ILogger<HomeController> logger, 
        IListingCompreterFactory listingFactory,
        IListingService listingService, 
        IErrorLogging errorLogging
        )
    {
        _logger = logger;
        _listingFactory = listingFactory;
        _listingService = listingService;
        _errorLogging = errorLogging;
    }

    [HttpGet]
    [Route("sort")]
    public async Task<ActionResult> GetSortedListings(SortMode sort, string city)
    {
        var existingListings = await _listingService.GetAllAsync();

        var factory = _listingFactory.createListingComparerFactory();
        var comparer = factory.GetComparer(sortMode: sort, city: city);

        existingListings.Sort(comparer);

        return base.Ok(existingListings);
    }

    [HttpPost]
    public async Task<ActionResult> Submit([FromBody] Listing listing)
    {
        List<Listing> existingListings = new List<Listing>();
        listing.Date = DateTime.Now.ToString("yyyy-MM-dd");
        User user = _context.Users.Where(u => u.Email == listing.Email).First();
        Console.WriteLine(user.Id);
        listing.UserId = user.Id;
        listing.User = user;

        try{
            await _listingService.AddAsync(listing);
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

        return base.Ok(await _listingService.GetAllAsync());
    }
}
