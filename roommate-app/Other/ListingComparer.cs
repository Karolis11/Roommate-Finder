using roommate_app.Models;

namespace roommate_app.Other;

public enum SortMode
{
    MaxPrice,
    NumRoommates,
    City
}

public class ListingComparer : IComparer<Listing>
{
    private SortMode sortMode;
    private String city;
    public ListingComparer(SortMode sortMode = SortMode.MaxPrice, String city = "Vilnius")
    {
        this.sortMode = sortMode;
        this.city = city;
    }
    public int Compare(Listing? x, Listing? y)
    {

        switch (sortMode) 
        {
            case SortMode.MaxPrice:
                if (x.City == this.city && y.City == this.city)
                {
                    return x.MaxPrice.CompareTo(y.MaxPrice);
                }
                else if (x.City == this.city)
                {
                    return -1;
                } 
                else
                    return x.MaxPrice.CompareTo(y.MaxPrice);
            case SortMode.NumRoommates:
                if (x.City == this.city && y.City == this.city)
                {
                    return x.RoommateCount.CompareTo(y.RoommateCount);
                }
                else if (x.City == this.city)
                {
                    return -1;
                }
                else
                    return x.RoommateCount.CompareTo(y.RoommateCount);
            case SortMode.City:
                return x.City.CompareTo(y.City);
            default:
                throw new NotImplementedException();
        }
    }
}
