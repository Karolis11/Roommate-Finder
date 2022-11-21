using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using roommate_app.Exceptions;
using roommate_app.Models;
using roommate_app.Other.ListingComparers;
using roommate_app.Services;
using System.Diagnostics.CodeAnalysis;

namespace roommate_app.Controllers;

[ApiController]
[Route("[controller]")]
[ExcludeFromCodeCoverage]
public class ListingController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IListingCompreterFactory _listingFactory;
    private readonly IErrorLogging _errorLogging;
    private readonly IListingService _listingService;
    private readonly IGenericService _genericService;

    public ListingController(
        ILogger<HomeController> logger,
        IListingCompreterFactory listingFactory,
        IErrorLogging errorLogging,
        IListingService listingService,
        IGenericService genericService
        )
    {
        _logger = logger;
        _listingFactory = listingFactory;
        _errorLogging = errorLogging;
        _listingService = listingService;
        _genericService = genericService;
    }

    [HttpGet]
    [Route("sort")]
    public async Task<ActionResult> GetSortedListings(SortMode sort, string city)
    {
        var existingListings = await _genericService.GetAllAsync<Listing>();

        var factory = _listingFactory.createListingComparerFactory();
        var comparer = factory.GetComparer(sortMode: sort, city: city);

        existingListings.Sort(comparer);

        return base.Ok(existingListings);
    }

    [HttpPost]
    public async Task<ActionResult> Submit([FromBody] Listing listing)
    {
        List<Listing> existingListings = new List<Listing>();
        List<User> existingUsers = await _genericService.GetAllAsync<User>();
        listing.Date = DateTime.Now.ToString("yyyy-MM-dd");
        User user = existingUsers.Where(u => u.Email == listing.Email).First();

        listing.UserId = user.Id;
        listing.User = user;

        try
        {
            await _genericService.AddAsync<Listing>(listing);
            existingListings.Add(listing);
        }
        catch (ArgumentNullException e)
        {
            _errorLogging.LogError(e.Message);
            _errorLogging.MessageError("Failed to load existing listing");
        }
        catch (SqlException e)
        {
            _errorLogging.LogError(e.Message);
            _errorLogging.MessageError("Could not insert a listing into the database.");
        }
        catch (Exception e)
        {
            _errorLogging.LogError(e.Message);
            _errorLogging.MessageError("Unexpected error, please restart the program");
        }

        return base.Ok(await _genericService.GetAllAsync<Listing>());
    }

    [HttpPost]
    [Route("update")]
    public async Task<ActionResult> UpdateListing([FromBody] Listing listing)
    {
        try
        {
            await _listingService.UpdateAsync(listing.Id, listing);
        }
        catch (SqlException e)
        {
            _errorLogging.LogError(e.Message);
            _errorLogging.MessageError("Could not update a listing (SQL database exception).");
        }
        catch (Exception e)
        {
            _errorLogging.LogError(e.Message);
            _errorLogging.MessageError("Unexpected error, please restart the program");
        }

        return base.Ok("Listing updated");
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult> DeleteListing(int Id)
    {
        try
        {
            await _genericService.DeleteAsync<Listing>(Id);
        }
        catch (SqlException e)
        {
            _errorLogging.LogError(e.Message);
            _errorLogging.MessageError("Could not delete a listing (SQL database exception).");
        }
        catch (Exception e)
        {
            _errorLogging.LogError(e.Message);
            _errorLogging.MessageError("Unexpected error, please restart the program");
        }

        return base.Ok("Listing deleted");
    }
}
