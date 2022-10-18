using Microsoft.AspNetCore.Mvc;
using roommate_app.Models;
using roommate_app.Other.ListingComparers;
using System.Text.Json;

namespace roommate_app.Controllers;

[ApiController]
[Route("[controller]")]
public class ListingController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IFileCreator _file;
    private readonly IListingCompreterFactory _listingFactory;

    public ListingController(ILogger<HomeController> logger, IFileCreator file, IListingCompreterFactory listingFactory)
    {
        _logger = logger;
        _file = file;
        _listingFactory = listingFactory;
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
        var existingListings = LoadJson();

        var factory = _listingFactory.createListingComparerFactory();
        var comparer = factory.GetComparer(sortMode: sort, city: city);

        existingListings.Sort(comparer);

        return base.Ok(existingListings);
    }

    [HttpPost]
    public ActionResult Submit([FromBody] Listing listing)
    {
        List<Listing> existingListings = LoadJson();
        existingListings.Add(listing);
        listing.Date = DateTime.Now.ToString("yyyy-MM-dd");
        string json = JsonSerializer.Serialize(existingListings);
        _file.Write("Data/listings.json", json, false);

        return base.Ok(existingListings);
    }
}
