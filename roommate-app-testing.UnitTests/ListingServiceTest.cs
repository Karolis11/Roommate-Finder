using Microsoft.EntityFrameworkCore;
using roommate_app.Controllers.Login;
using roommate_app.Controllers.Registration;
using roommate_app.Data;
using roommate_app.Exceptions;
using roommate_app.Models;
using roommate_app.Other.FileCreator;
using roommate_app.Services;

namespace roommate_app_testing.UnitTests
{
    public class ListingServiceTest
    {
        [Fact]
        public void ListingServiceRetrievesListingsByUserIdCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            var listingService = new ListingService(context);

            var user = new User()
            {
                Id = 16,
                FirstName = "a",
                LastName = "b",
                Email = "abc@email.com",
                Password = "abc",
                City = "c"
            };

            context.Users.Add(user);

            context.Listings.Add(new Listing()
            {
                Id = 1,
                FirstName = "a",
                LastName = "b",
                Email = "abc@email.com",
                Phone = "123",
                City = "c",
                RoommateCount = 1,
                MaxPrice = 100,
                ExtraComment = "",
                Date = "10",
                UserId = 16,
                User = user
            });

            context.Listings.Add(new Listing()
            {
                Id = 2,
                FirstName = "a",
                LastName = "b",
                Email = "abc@email.com",
                Phone = "123",
                City = "c",
                RoommateCount = 2,
                MaxPrice = 200,
                ExtraComment = "",
                Date = "10",
                UserId = 16,
                User = user
            });

            context.SaveChanges();

            var actualListings = listingService.GetByUserId(16);

            Assert.Equal(2, actualListings.Count());
            var actualIds = new[] { actualListings[0].Id, actualListings[1].Id };
            Assert.Contains(1, actualIds);
            Assert.Contains(2, actualIds);
        }

        [Fact]
        public async void ListingServiceUpdatesListingsCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            var listingService = new ListingService(context);

            var user = new User()
            {
                Id = 17,
                FirstName = "a",
                LastName = "b",
                Email = "abc@email.com",
                Password = "abc",
                City = "c"
            };

            context.Users.Add(user);

            context.Listings.Add(new Listing()
            {
                Id = 3,
                FirstName = "a",
                LastName = "b",
                Email = "abc@email.com",
                Phone = "123",
                City = "c",
                RoommateCount = 2,
                MaxPrice = 200,
                ExtraComment = "",
                Date = "10",
                UserId = 17,
                User = user
            });

            context.SaveChanges();

            var newListing = new Listing()
            {
                Id = 3,
                FirstName = "a",
                LastName = "bc",
                Email = "abc@email.com",
                Phone = "1234",
                City = "c",
                RoommateCount = 3,
                MaxPrice = 300,
                ExtraComment = "abc",
                Date = "10",
                UserId = 17,
                User = user
            };

            await listingService.UpdateAsync(3, newListing);

            var updatedListing = context.Listings.Where(l => l.Id == 3).First();

            Assert.Equal("b", updatedListing.LastName);
            Assert.Equal("1234", updatedListing.Phone);
            Assert.Equal(3, updatedListing.RoommateCount);
            Assert.Equal(300, updatedListing.MaxPrice);
            Assert.Equal("abc", updatedListing.ExtraComment);
        }
    }
}
