using roommate_app.Models;

namespace roommate_app.Other.ListingComparers;

public class ListingCityComparer : ListingComparer
{

    public ListingCityComparer() {}

    public override int Compare(Listing? x, Listing? y)
    {
        return x.City.CompareTo(y.City);
    }
}

