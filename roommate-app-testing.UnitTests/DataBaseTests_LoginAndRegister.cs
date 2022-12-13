using Microsoft.EntityFrameworkCore;
using roommate_app.Controllers.Login;
using roommate_app.Controllers.Registration;
using roommate_app.Data;
using roommate_app.Exceptions;
using roommate_app.Models;
using roommate_app.Other.FileCreator;

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
}
