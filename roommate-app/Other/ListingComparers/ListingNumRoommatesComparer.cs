using roommate_app.Models;

namespace roommate_app.Other.ListingComparers;

public class ListingNumRoommatesComparer : ListingComparer
{
    public ListingNumRoommatesComparer(string city) : base(city) {}

    public override int Compare(Listing? x, Listing? y)
    {
        if (x.City == this.city && y.City == this.city)
        {
            return x.RoommateCount.CompareTo(y.RoommateCount);
        }
        else if (x.City == this.city)
        {
            return -1;
        }
        else
        {
            return x.RoommateCount.CompareTo(y.RoommateCount);
        }
    }
}