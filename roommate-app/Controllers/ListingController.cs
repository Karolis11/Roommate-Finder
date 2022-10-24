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
    private readonly ApplicationDbContext _context;

    public ListingController(ILogger<HomeController> logger, IListingCompreterFactory listingFactory, ApplicationDbContext context)
    {
        _logger = logger;
        _listingFactory = listingFactory;
        _context = context;
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
        listing.Date = DateTime.Now.ToString("yyyy-MM-dd");
        _context.Listings.Add(listing);
        _context.SaveChanges();

        return base.Ok(_context.Listings.ToList());
    }
}
