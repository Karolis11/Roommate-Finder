using roommate_app.Models;

namespace roommate_app.Other.ListingComparers;

public class ListingMaxPriceComparer : ListingComparer
{

    public ListingMaxPriceComparer(string city) : base(city) { }

    public override int Compare(Listing? x, Listing? y)
    {
        if (x.City == this.city && y.City == this.city)
        {
            return x.MaxPrice.CompareTo(y.MaxPrice);
        }
        else if (x.City == this.city)
        {
            return -1;
        }
        else
        {
            return x.MaxPrice.CompareTo(y.MaxPrice);
        }
    }
}

