namespace roommate_app.Other.ListingComparers;

public class ListingComparerFactory
{
    public ListingComparerFactory() {}

    public ListingComparer GetComparer(SortMode sortMode = SortMode.MaxPrice, string city = "Vilnius")
    {
        switch (sortMode)
        {
            case SortMode.MaxPrice:
                return new ListingMaxPriceComparer(city);
            case SortMode.NumRoommates:
                return new ListingNumRoommatesComparer(city);
            case SortMode.City:
                return new ListingCityComparer();
            default:
                throw new NotImplementedException();
        }
    }
}

