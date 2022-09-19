using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using roommate_app.Models;

namespace roommate_app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListingController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ListingController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "works";
        }

        [HttpPost]
        public JsonResult Submit([FromBody] ListingSubmission listing)
        {
            Console.WriteLine(listing.ToString());
            return Json(listing);
        }
    }
}