using roommate_app.Other.ListingComparers;
using System.Diagnostics.CodeAnalysis;

public interface IListingCompreterFactory
{
    ListingComparerFactory createListingComparerFactory();
}

public class ListingComparerFactory : IListingCompreterFactory
{
    ListingComparerFactory IListingCompreterFactory.createListingComparerFactory()
    {
        ListingComparerFactory factory = new ListingComparerFactory();
        return factory;
    }

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