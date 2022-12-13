using roommate_app.Models;
using roommate_app.Other.ListingComparers;

namespace roommate_app_testing.UnitTests
{
    public class ListingComparerFactoryTests
    {
        [Fact]
        public void ListingComparerFactoryGeneratesClassesCorrectly()
        {
            var factory = new ListingComparerFactory();
            Assert.IsType<ListingMaxPriceComparer>(factory.GetComparer(SortMode.MaxPrice));
            Assert.IsType<ListingNumRoommatesComparer>(factory.GetComparer(SortMode.NumRoommates));
            Assert.IsType<ListingCityComparer>(factory.GetComparer(SortMode.City));
        }

        [Fact]
        public void MaxPriceComparerSortsListingsCorrectly()
        {
            var comparer = new ListingMaxPriceComparer("Vilnius");
            List<Listing> listings = new List<Listing>();
            listings.Add(new Listing { City = "Klaipeda", MaxPrice = 3 });
            listings.Add(new Listing { City = "Klaipeda", MaxPrice = 1 });
            listings.Add(new Listing { City = "Klaipeda", MaxPrice = 2 });
            listings.Add(new Listing { City = "Vilnius", MaxPrice = 2 });
            listings.Add(new Listing { City = "Vilnius", MaxPrice = 1 });
            listings.Sort(comparer);

            List<Listing> expectedListings = new List<Listing>();
            expectedListings.Add(new Listing { City = "Vilnius", MaxPrice = 1 });
            expectedListings.Add(new Listing { City = "Vilnius", MaxPrice = 2 });
            expectedListings.Add(new Listing { City = "Klaipeda", MaxPrice = 1 });
            expectedListings.Add(new Listing { City = "Klaipeda", MaxPrice = 2 });
            expectedListings.Add(new Listing { City = "Klaipeda", MaxPrice = 3 });

            for (int i = 0; i < listings.Count; i++)
            {
                Assert.Equal(expectedListings[i].City, listings[i].City);
                Assert.Equal(expectedListings[i].MaxPrice, listings[i].MaxPrice);
            }
        }

        [Fact]
        public void NumRoommatesComparerSortsListingsCorrectly()
        {
            var comparer = new ListingNumRoommatesComparer("Vilnius");
            List<Listing> listings = new List<Listing>();
            listings.Add(new Listing { City = "Klaipeda", RoommateCount = 3 });
            listings.Add(new Listing { City = "Klaipeda", RoommateCount = 1 });
            listings.Add(new Listing { City = "Klaipeda", RoommateCount = 2 });
            listings.Add(new Listing { City = "Vilnius", RoommateCount = 2 });
            listings.Add(new Listing { City = "Vilnius", RoommateCount = 1 });
            listings.Sort(comparer);

            List<Listing> expectedListings = new List<Listing>();
            expectedListings.Add(new Listing { City = "Vilnius", RoommateCount = 1 });
            expectedListings.Add(new Listing { City = "Vilnius", RoommateCount = 2 });
            expectedListings.Add(new Listing { City = "Klaipeda", RoommateCount = 1 });
            expectedListings.Add(new Listing { City = "Klaipeda", RoommateCount = 2 });
            expectedListings.Add(new Listing { City = "Klaipeda", RoommateCount = 3 });

            for (int i = 0; i < listings.Count; i++)
            {
                Assert.Equal(expectedListings[i].City, listings[i].City);
                Assert.Equal(expectedListings[i].MaxPrice, listings[i].MaxPrice);
            }
        }

        [Fact]
        public void CityComparerSortsListingsCorrectly()
        {
            var comparer = new ListingCityComparer();
            List<Listing> listings = new List<Listing>();
            listings.Add(new Listing { City = "C" });
            listings.Add(new Listing { City = "E" });
            listings.Add(new Listing { City = "B" });
            listings.Add(new Listing { City = "A" });
            listings.Add(new Listing { City = "D" });
            listings.Sort(comparer);

            List<Listing> expectedListings = new List<Listing>();
            expectedListings.Add(new Listing { City = "A" });
            expectedListings.Add(new Listing { City = "B" });
            expectedListings.Add(new Listing { City = "C" });
            expectedListings.Add(new Listing { City = "D" });
            expectedListings.Add(new Listing { City = "E" });

            for (int i = 0; i < listings.Count; i++)
            {
                Assert.Equal(expectedListings[i].City, listings[i].City);
            }
        }
    }
}
