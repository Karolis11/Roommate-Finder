namespace roommate_app.Models
{
    public class Listing
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string city { get; set; }
        public int roommateCount { get; set; }
        public float maxPrice { get; set; }
        public string phone { get; set;}
        public string extraComment { get; set; }
    }
}
