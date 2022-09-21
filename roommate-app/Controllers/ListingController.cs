using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using roommate_app.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

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

        public List<Listing> LoadJson()
        {
            using (StreamReader r = new StreamReader("./Data/listings.json"))
            {
                string json = r.ReadToEnd();
                List<Listing> listings = JsonConvert.DeserializeObject<List<Listing>>(json);
                return listings;
            }
        }

        [HttpGet]
        public JsonResult Get()
        {
            //string[] lines = System.IO.File.ReadAllLines(@"../react-client/src/data/failas.txt");
            return Json(this.LoadJson());
        }

        [HttpPost]
        public JsonResult Submit([FromBody] Listing listing)
        {
            List<Listing> existingListings = LoadJson();
            existingListings.Add(listing);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(existingListings);
            System.IO.StreamWriter tsw = new System.IO.StreamWriter("Data/listings.json", false);
            tsw.WriteLine(json);
            //tsw.WriteLine('\n');
            tsw.Close();
            return Json(existingListings);
        }
    }
}