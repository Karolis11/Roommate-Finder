using Microsoft.EntityFrameworkCore;
using roommate_app.Controllers.Authentication;
using roommate_app.Controllers.Login;
using roommate_app.Controllers.Registration;
using roommate_app.Data;
using roommate_app.Exceptions;
using roommate_app.Models;
using roommate_app.Other.FileCreator;
using roommate_app.Services;

namespace roommate_app_testing.UnitTests
{
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
        [Fact]
        public void UserServiceGetsUserIdCorrectlyFromToken()
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

            var user = userService.GetById(1);
            var token = userService.GenerateJwtToken(user);

            var validatedUserId = userService.GetValidatedId(token);

            Assert.Equal(1, validatedUserId);
        }
    }
}
