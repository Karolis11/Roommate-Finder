using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using roommate_app.Controllers.Login;
using roommate_app.Controllers.Registration;
using roommate_app.Data;
using roommate_app.Exceptions;
using roommate_app.Models;
using roommate_app.Other.FileCreator;

namespace roommate_app_testing.UnitTests
{
    public class LoginTests
    {

        [Fact]
        public void isLoggedIn_EmailAndPasswordCorrect_ReturnsTrue()
        {
            // Arrange
            var file = new FileCreator();
            var errorLoging = new ErrorLogging(file);
            var loginController = new LoginController(errorLoging, file);

            // Act
            OkObjectResult result = loginController.Submit(new User { Email = "abc@gmail.com", Password = "abcdef" });

            // Assert
            Assert.NotNull(result);

            var model = result.Value as LoginResponse;
            Assert.NotNull(model);

            Assert.True(model.IsSuccess);
        }

        [Fact]
        public void isLoggedIn_EmailCorrectAndPasswordNotCorrect_ReturnsFalse()
        {
            // Arrange
            var file = new FileCreator();
            var errorLoging = new ErrorLogging(file);
            var loginController = new LoginController(errorLoging, file);

            // Act
            OkObjectResult result = loginController.Submit(new User { Email = "abc@gmail.com", Password = "abcdef1" });

            // Assert
            Assert.NotNull(result);

            var model = result.Value as LoginResponse;
            Assert.NotNull(model);

            Assert.False(model.IsSuccess);
        }

        [Fact]
        public void isLoggedIn_EmailNotCorrectAndPasswordCorrect_ReturnsFalse()
        {
            // Arrange
            var file = new FileCreator();
            var errorLoging = new ErrorLogging(file);
            var loginController = new LoginController(errorLoging, file);

            // Act
            OkObjectResult result = loginController.Submit(new User { Email = "abc1@gmail.com", Password = "abcdef" });

            // Assert
            Assert.NotNull(result);

            var model = result.Value as LoginResponse;
            Assert.NotNull(model);

            Assert.False(model.IsSuccess);
        }

        [Fact]
        public void isLoggedIn_EmailAndPasswordNotCorrect_ReturnsFalse()
        {
            // Arrange
            var file = new FileCreator();
            var errorLoging = new ErrorLogging(file);
            var loginController = new LoginController(errorLoging, file);

            // Act
            OkObjectResult result = loginController.Submit(new User { Email = "abc1@gmail.com", Password = "abcdef1" });

            // Assert
            Assert.NotNull(result);

            var model = result.Value as LoginResponse;
            Assert.NotNull(model);

            Assert.False(model.IsSuccess);
        }
    }

    // TODO: PASS CORRECT DB TO RegistrationController
    // TESTS BELOW FAILS

    public class RegisterTests
    {
        [Fact]
        public void isRegistered_NewEmail_ReturnsTrue()
        {
            // Arrange
            var options = new DbContextOptions<ApplicationDbContext>();
            var context = new ApplicationDbContext(options);
            var registrationController = new RegistrationController(context);

            // Act
            var result = registrationController.Submit(new User { Email = "abc1@abc.com", Password = "" });

            // Assert
            Assert.NotNull(result);

            var model = result.Value as RegistrationResponse;
            Assert.NotNull(model);

            Assert.True(model.IsSuccess);
        }

        [Fact]
        public void isRegistered_UsedEmail_ReturnsFalse()
        {
            // Arrange
            var options = new DbContextOptions<ApplicationDbContext>();
            var context = new ApplicationDbContext(options);
            var registrationController = new RegistrationController(context);

            // Act
            var result = registrationController.Submit(new User { Email = "abc@abc.com", Password = "" });

            // Assert
            Assert.NotNull(result);

            var model = result.Value as RegistrationResponse;
            Assert.NotNull(model);

            Assert.False(model.IsSuccess);
        }
    }
}