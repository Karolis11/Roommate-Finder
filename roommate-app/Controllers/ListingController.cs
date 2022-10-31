using Microsoft.AspNetCore.Mvc;
using roommate_app.Models;
using roommate_app.Data;
using roommate_app.Other.ListingComparers;
using System.Text.Json;

namespace roommate_app.Controllers;

[ApiController]
[Route("[controller]")]
public class ListingController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IListingCompreterFactory _listingFactory;

    private ErrorLogging errorLogging;
    private readonly ApplicationDbContext _context;
    private IFileCreator _file;

    public ListingController(ILogger<HomeController> logger, IListingCompreterFactory listingFactory, ApplicationDbContext context, IFileCreator file)
    {
        _logger = logger;
        _listingFactory = listingFactory;
        _context = context;
        _file = file;
        errorLogging = new ErrorLogging(_file);
    }

    private List<Listing> LoadJson()
    {
        string json = _file.ReadToEndFile("Data/listings.json");
        List<Listing> listings = JsonSerializer.Deserialize<List<Listing>>(json);
        return listings;
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
            errorLogging.logError(e.Message);
            errorLogging.messageError("Failed to load existing listing");
        }
        catch(FileNotFoundException e){
            errorLogging.logError(e.Message);
            errorLogging.messageError("The listings were not found");
        }
        catch(Exception e){
            errorLogging.logError(e.Message);
            errorLogging.messageError("Unexpected error, please restart the program");
        }

        return base.Ok(_context.Listings.ToList());
    }
}
