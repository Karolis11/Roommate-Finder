using System.Diagnostics.CodeAnalysis;

namespace roommate_app.Other.ListingComparers;

[ExcludeFromCodeCoverage]
public class SortListingsRequestBody
{
    public SortMode Sort { get; set; }
    public string City { get; set; }
}
