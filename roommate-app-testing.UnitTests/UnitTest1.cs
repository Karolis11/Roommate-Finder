using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using roommate_app.Controllers.Authentication;
using roommate_app.Controllers.Login;
using roommate_app.Controllers.Registration;
using roommate_app.Data;
using roommate_app.Exceptions;
using roommate_app.Models;
using roommate_app.Other.FileCreator;
using roommate_app.Other.ListingComparers;
using roommate_app.Services;
using roommate_app.Other.Validation;

namespace roommate_app_testing.UnitTests
{
    public class DataBaseTests_LoginAndRegister
    {

        [Fact]
        public void Register_NewEmail_ReturnsTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var _dbContext = new ApplicationDbContext(options);
            var genericService = new GenericService(_dbContext);
            var registrationController = new RegistrationController(genericService);

            // Act
            var result = registrationController.Submit(new User { City = "", FirstName = "", LastName = "", Email = "abc@abc.com", Password = "password" });
            var model = result.Result.Value as RegistrationResponse;

            // Assert
            Assert.NotNull(model);
            Assert.True(model.IsSuccess);
        }

        [Fact]
        public void Register_UsedEmail_ReturnsFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var _dbContext = new ApplicationDbContext(options);
            var genericService = new GenericService(_dbContext);
            var registrationController = new RegistrationController(genericService);

            // Act
            var submit = registrationController.Submit(new User { City = "", FirstName = "", LastName = "", Email = "abc@abc.com", Password = "password" });
            var result = registrationController.Submit(new User { City = "", FirstName = "", LastName = "", Email = "abc@abc.com", Password = "password" });
            var model = result.Result.Value as RegistrationResponse;

            // Assert
            Assert.NotNull(model);
            Assert.False(model.IsSuccess);
        }

        [Fact]
        public void Login_EmailAndPasswordCorrect_ReturnsTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var _dbContext = new ApplicationDbContext(options);
            var genericService = new GenericService(_dbContext);
            var file = new FileCreator();
            var errorLoging = new ErrorLogging(file);
            var loginController = new LoginController(errorLoging, genericService);
            var registrationController = new RegistrationController(genericService);

            // Act
            var submit = registrationController.Submit(new User { City = "", FirstName = "", LastName = "", Email = "abc@abc.com", Password = "password" });
            var result = loginController.Submit(new User { Email = "abc@abc.com", Password = "password" });
            var model = result.Result.Value as LoginResponse;

            // Assert
            Assert.NotNull(model);
            Assert.True(model.IsSuccess);
        }
        [Fact]
        public void Login_EmailCorrectAndPasswordNotCorrect_ReturnsFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var _dbContext = new ApplicationDbContext(options);
            var genericService = new GenericService(_dbContext);
            var file = new FileCreator();
            var errorLoging = new ErrorLogging(file);
            var loginController = new LoginController(errorLoging, genericService);
            var registrationController = new RegistrationController(genericService);

            // Act
            var submit = registrationController.Submit(new User { City = "", FirstName = "", LastName = "", Email = "abc@abc.com", Password = "password" });
            var result = loginController.Submit(new User { Email = "abc@abc.com", Password = "password1" });
            var model = result.Result.Value as LoginResponse;

            // Assert
            Assert.NotNull(model);
            Assert.False(model.IsSuccess);
        }

        [Fact]
        public void Login_EmailNotCorrectAndPasswordCorrect_ReturnsFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var _dbContext = new ApplicationDbContext(options);
            var genericService = new GenericService(_dbContext);
            var file = new FileCreator();
            var errorLoging = new ErrorLogging(file);
            var loginController = new LoginController(errorLoging, genericService);
            var registrationController = new RegistrationController(genericService);

            // Act
            var submit = registrationController.Submit(new User { City = "", FirstName = "", LastName = "", Email = "abc@abc.com", Password = "password" });
            var result = loginController.Submit(new User { Email = "abc1@abc.com", Password = "password" });
            var model = result.Result.Value as LoginResponse;

            // Assert
            Assert.NotNull(model);
            Assert.False(model.IsSuccess);
        }

        [Fact]
        public void Login_EmailAndPasswordNotCorrect_ReturnsFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var _dbContext = new ApplicationDbContext(options);
            var genericService = new GenericService(_dbContext);
            var file = new FileCreator();
            var errorLoging = new ErrorLogging(file);
            var loginController = new LoginController(errorLoging, genericService);
            var registrationController = new RegistrationController(genericService);

            // Act
            var submit = registrationController.Submit(new User { City = "", FirstName = "", LastName = "", Email = "abc@abc.com", Password = "password" });
            var result = loginController.Submit(new User { Email = "abc1@abc.com", Password = "password1" });
            var model = result.Result.Value as LoginResponse;

            // Assert
            Assert.NotNull(model);
            Assert.False(model.IsSuccess);
        }
    }

    public class ComparersTest_FactoryAndComparers
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

    public class GenericServiceTest
    {
        [Fact]
        public void GenericServiceRetrievesAllUsersCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            var genericService = new GenericService(context);

            context.Users.Add(new User()
            {
                Id = 1,
                FirstName = "a",
                LastName = "b",
                Email = "ab@email.com",
                Password = "abc",
                City = "c"
            });
            context.Users.Add(new User()
            {
                Id = 2,
                FirstName = "c",
                LastName = "d",
                Email = "cd@email.com",
                Password = "abc",
                City = "c"
            });
            context.SaveChanges();

            var users = genericService.GetAllAsync<User>();
            var expectedUsers = new List<User>();
            expectedUsers.Add(new User()
            {
                Id = 1,
                FirstName = "a",
                LastName = "b",
                Email = "ab@email.com",
                Password = "abc",
                City = "c"
            });
            expectedUsers.Add(new User()
            {
                Id = 2,
                FirstName = "c",
                LastName = "d",
                Email = "cd@email.com",
                Password = "abc",
                City = "c"
            });

            for (int i = 0; i < expectedUsers.Count; i++)
            {
                Assert.Equal(expectedUsers[i].Id, users.Result[i].Id);
            }
        }

        [Fact]
        public void GenericServiceRetrievesAUserByIdCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            var genericService = new GenericService(context);

            context.Users.Add(new User()
            {
                Id = 15,
                FirstName = "a",
                LastName = "b",
                Email = "ab@email.com",
                Password = "abc",
                City = "c"
            });

            var user = genericService.GetById<User>(15);
            var expectedUser = (new User()
            {
                Id = 15,
                FirstName = "a",
                LastName = "b",
                Email = "ab@email.com",
                Password = "abc",
                City = "c"
            });

            Assert.Equal(expectedUser.Id, user.Id);
        }

        [Fact]
        public async void GenericServiceUpdatesAUserByIdCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            var genericService = new GenericService(context);

            context.Users.Add(new User()
            {
                Id = 15,
                FirstName = "a",
                LastName = "b",
                Email = "ab@email.com",
                Password = "abc",
                City = "c"
            });

            await genericService.UpdateAsync<User>(15, new User()
            {
                Id = 15,
                FirstName = "a",
                LastName = "b",
                Email = "abcd@email.com",
                Password = "abcdef",
                City = "c"
            });

            var actualUser = context.Users.Find(15);

            Assert.Equal("abcd@email.com", actualUser.Email);
            Assert.Equal("abcdef", actualUser.Password);
            Assert.Equal("a", actualUser.FirstName);
            Assert.Equal("b", actualUser.LastName);
            Assert.Equal("c", actualUser.City);
        }

        [Fact]
        public async void GenericServiceDeletesAUserByIdCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            var genericService = new GenericService(context);

            context.Users.Add(new User()
            {
                Id = 15,
                FirstName = "a",
                LastName = "b",
                Email = "ab@email.com",
                Password = "abc",
                City = "c"
            });

            Assert.NotNull(genericService.GetById<User>(15));

            await genericService.DeleteAsync<User>(15);

            Assert.Null(genericService.GetById<User>(15));
        }
    }

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

    public class UserServiceTest
    {
        [Fact]
        public void UserServiceSucceedsInAuthenticatingUserCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();

            context.Users.Add(new User
            {
                Id = 1,
                FirstName = "a",
                LastName = "b",
                Email = "abc@email.com",
                Password = "abc",
                City = "c"
            });
            context.SaveChanges();

            var userService = new UserService(context);

            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "abc@email.com",
                Password = "abc"
            };

            var authenticateResponse = userService.Authenticate(authenticateRequest);

            Assert.NotNull(authenticateResponse.Token);
        }

        [Fact]
        public void UserServiceFailsToAuthenticateCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();

            context.Users.Add(new User
            {
                Id = 1,
                FirstName = "a",
                LastName = "b",
                Email = "abc@email.com",
                Password = "abc",
                City = "c"
            });
            context.SaveChanges();

            var userService = new UserService(context);

            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "abc@email.com",
                Password = "abcd"
            };

            var authenticateResponse = userService.Authenticate(authenticateRequest);

            Assert.Null(authenticateResponse.Token);
        }

        [Fact]
        public void UserServiceRetrievesUserByIdCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();

            context.Users.Add(new User
            {
                Id = 1,
                FirstName = "a",
                LastName = "b",
                Email = "abc@email.com",
                Password = "abc",
                City = "c"
            });
            context.SaveChanges();

            var userService = new UserService(context);

            var actualUser = userService.GetById(1);

            Assert.Equal("abc@email.com", actualUser.Email);
            Assert.Equal("abc", actualUser.Password);
        }
    }

    public class ListingValidationTest
    {
        [Fact]
        public void ValidateNameCorrectly()
        {
            // Arrange
            Listing list = new Listing { FirstName = "Test-Test", LastName = "Test'Test" };
            ListingValidation ListingValidation = new ListingValidation();
            // Act
            var result = ListingValidation.ValidateName(list);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateEmailCorrectly()
        {
            // Arrange
            Listing list = new Listing { Email = "abc@abc.com" };
            ListingValidation ListingValidation = new ListingValidation();
            // Act
            var result = ListingValidation.ValidateEmail(list);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateCityCorrectly()
        {
            // Arrange
            Listing list = new Listing { City = "Vilnius" };
            ListingValidation ListingValidation = new ListingValidation();
            // Act
            var result = ListingValidation.ValidateCity(list);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateRoommateCountCorrectly()
        {
            // Arrange
            Listing list = new Listing { RoommateCount = 3 };
            ListingValidation ListingValidation = new ListingValidation();
            // Act
            var result = ListingValidation.ValidateRoommateCount(list);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidatePhoneNumberCorrectly()
        {
            // Arrange
            Listing list = new Listing { Phone = "+37060026689" };
            ListingValidation ListingValidation = new ListingValidation();
            // Act
            var result = ListingValidation.ValidatePhoneNumber(list);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateExtraCommentCorrectly()
        {
            // Arrange
            Listing list = new Listing { ExtraComment = "Test Test Test" };
            ListingValidation ListingValidation = new ListingValidation();
            // Act
            var result = ListingValidation.ValidateExtraComment(list);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateMaxPriceCorrectly()
        {
            // Arrange
            Listing list = new Listing { MaxPrice = 1 };
            ListingValidation ListingValidation = new ListingValidation();
            // Act
            var result = ListingValidation.ValidateMaximumPrice(list);
            // Assert
            Assert.True(result);
        }
    }
}
    /*public class JWTMiddlewareTest
    {
        [Fact]
        public void JWTMiddlewareValidatesCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();

            context.Users.Add(new User
            {
                Id = 1,
                FirstName = "a",
                LastName = "b",
                Email = "abc@email.com",
                Password = "abc",
                City = "c"
            });
            context.SaveChanges();

            var requestDelegate = new RequestDelegate((innerContext) => Task.FromResult(0));
            var jwtMiddleware = new JwtMiddleware(requestDelegate);
            var userService = new UserService(context);

            var user = userService.GetById(1);
            var token = userService.GenerateJwtToken(user);

            var validatedUserId = jwtMiddleware.GetValidatedId(userService, token);

            Assert.Equal(1, validatedUserId);
        }
    }
}*/
