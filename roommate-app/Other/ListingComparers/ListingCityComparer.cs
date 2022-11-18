using roommate_app.Models;
using System.Diagnostics.CodeAnalysis;

namespace roommate_app.Other.ListingComparers;
public class ListingCityComparer : ListingComparer
{

    public override int Compare(Listing? x, Listing? y)
    {
        return x.City.CompareTo(y.City);
    }
}

