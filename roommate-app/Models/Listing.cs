namespace roommate_app.Models
{
    public class Listing
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Phone { get; set;}
        public int RoommateCount { get; set; }
        public float MaxPrice { get; set; }
        public string ExtraComment { get; set; }
    }
}
