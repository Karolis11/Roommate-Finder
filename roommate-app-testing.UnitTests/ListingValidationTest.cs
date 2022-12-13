using Microsoft.EntityFrameworkCore;
using roommate_app.Controllers.Authentication;
using roommate_app.Controllers.Login;
using roommate_app.Controllers.Registration;
using roommate_app.Data;
using roommate_app.Exceptions;
using roommate_app.Models;
using roommate_app.Other.FileCreator;
using roommate_app.Services;
using roommate_app.Other.Validation;

namespace roommate_app_testing.UnitTests
{
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
