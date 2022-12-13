using Microsoft.EntityFrameworkCore;
using roommate_app.Data;
using roommate_app.Models;
using roommate_app.Other.ListingComparers;

namespace roommate_app_testing.UnitTests
{
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
}
