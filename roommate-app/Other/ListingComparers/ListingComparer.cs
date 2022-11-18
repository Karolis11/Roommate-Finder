using roommate_app.Models;
using System.Diagnostics.CodeAnalysis;

namespace roommate_app.Other.ListingComparers;
public abstract class ListingComparer : IComparer<Listing>
{
    protected String city;
    public ListingComparer(String city = "Vilnius")
    {
        this.city = city;
    }

    public abstract int Compare(Listing? x, Listing? y);
}
