using System.ComponentModel.DataAnnotations.Schema;

namespace roommate_app.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public int RoommateCount { get; set; }
        public int MaxPrice { get; set; }
        public string ExtraComment { get; set; }
        public string Date { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
    static public class ListingExtension
    {
        static public string FullName(this Listing value)
        => $"{value.FirstName} {value.LastName}";
    }
}

