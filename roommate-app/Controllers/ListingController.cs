using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using roommate_app.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        public string[] Get()
        {
            string[] lines = System.IO.File.ReadAllLines(@"../react-client/src/data/failas.txt");
            return lines;
        }

        [HttpPost]
        public JsonResult Submit([FromBody] Listing listing)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(new { listing });
            System.IO.StreamWriter tsw = new System.IO.StreamWriter("../react-client/src/data/failas.txt", true);
            tsw.WriteLine(json);
            tsw.WriteLine('\n');
            tsw.Close();
            return Json(listing);
        }
    }
}