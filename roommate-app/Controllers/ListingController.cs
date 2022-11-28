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
    public async Task<JsonResult> GetSortedListings(SortMode sort, string city)
    {
        var existingListings = await _genericService.GetAllAsync<Listing>();

        var factory = _listingFactory.createListingComparerFactory();
        var comparer = factory.GetComparer(sortMode: sort, city: city);

        existingListings.Sort(comparer);

        var response = new JsonResult(existingListings);
        response.StatusCode = 200;

        return response;
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

        var response = new JsonResult(new Object());
        var isInternalServerError = false;
        var message = "";

        try
        {
            await _genericService.AddAsync<Listing>(listing);
            existingListings.Add(listing);
        }
        catch (ArgumentNullException e)
        {
            _errorLogging.LogError(e.Message);
            _errorLogging.MessageError("Failed to load existing listing");
            isInternalServerError = true;
            message = "Failed to load existing listing";
        }
        catch (SqlException e)
        {
            _errorLogging.LogError(e.Message);
            _errorLogging.MessageError("Could not insert a listing into the database.");
            isInternalServerError = true;
            message = "Could not insert a listing into the database.";
        }
        catch (Exception e)
        {
            _errorLogging.LogError(e.Message);
            _errorLogging.MessageError("Unexpected error, please restart the program");
            isInternalServerError = true;
            message = "Unexpected error, please restart the program";
        }

        if (isInternalServerError)
        {
            response = new JsonResult(message);
            response.StatusCode = 500;
        } else
        {
            response = new JsonResult(await _genericService.GetAllAsync<Listing>());
            response.StatusCode = 201;
        }

        return response;
    }

    [HttpPost]
    [Route("update")]
    public async Task<ActionResult> UpdateListing([FromBody] Listing listing)
    {
        var response = new JsonResult(new Object());
        var isInternalServerError = false;
        var message = "";

        try
        {
            await _listingService.UpdateAsync(listing.Id, listing);
        }
        catch (SqlException e)
        {
            _errorLogging.LogError(e.Message);
            _errorLogging.MessageError("Could not update a listing (SQL database exception).");
            isInternalServerError = true;
            message = "Could not update a listing (SQL database exception).";
        }
        catch (Exception e)
        {
            _errorLogging.LogError(e.Message);
            _errorLogging.MessageError("Unexpected error, please restart the program");
            isInternalServerError = true;
            message = "Unexpected error, please restart the program";
        }

        if (isInternalServerError)
        {
            response = new JsonResult(message);
            response.StatusCode = 500;
        } else
        {
            response = new JsonResult("Listing updated");
            response.StatusCode = 200;
        }

        return response;
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteListing(int Id)
    {
        var response = new JsonResult(new Object());
        var isInternalServerError = false;
        var message = "";

        try
        {
            await _genericService.DeleteAsync<Listing>(Id);
        }
        catch (SqlException e)
        {
            _errorLogging.LogError(e.Message);
            _errorLogging.MessageError("Could not update a listing (SQL database exception).");
            isInternalServerError = true;
            message = "Could not update a listing (SQL database exception).";
        }
        catch (Exception e)
        {
            _errorLogging.LogError(e.Message);
            _errorLogging.MessageError("Unexpected error, please restart the program");
            isInternalServerError = true;
            message = "Unexpected error, please restart the program";
        }

        if (isInternalServerError)
        {
            response = new JsonResult(message);
            response.StatusCode = 500;
        }
        else
        {
            response = new JsonResult("Listing deleted");
            response.StatusCode = 200;
        }

        return response;
    }
}
