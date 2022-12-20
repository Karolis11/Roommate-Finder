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
        public void ValidateNameTrueCase()
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
        public void ValidateNameFalseCase()
        {
            // Arrange
            Listing list = new Listing { FirstName = "123", LastName = "456" };
            ListingValidation ListingValidation = new ListingValidation();
            // Act
            var result = ListingValidation.ValidateName(list);
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateEmailTrueCase()
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
        public void ValidateEmailFalseCase()
        {
            // Arrange
            Listing list = new Listing { Email = "abc.com" };
            ListingValidation ListingValidation = new ListingValidation();
            // Act
            var result = ListingValidation.ValidateEmail(list);
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidateEmailFalseCase2()
        {
            // Arrange
            Listing list = new Listing { Email = "" };
            ListingValidation ListingValidation = new ListingValidation();
            // Act
            var result = ListingValidation.ValidateEmail(list);
            // Assert
            Assert.False(result);
        }


        [Fact]
        public void ValidateCityTrueCase()
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
        public void ValidateCityFalseCase()
        {
            // Arrange
            Listing list = new Listing { City = "123" };
            ListingValidation ListingValidation = new ListingValidation();
            // Act
            var result = ListingValidation.ValidateCity(list);
            // Assert
            Assert.False(result);
        }
        [Fact]
        public void ValidateCityFalseCase2()
        {
            // Arrange
            Listing list = new Listing { City = "" };
            ListingValidation ListingValidation = new ListingValidation();
            // Act
            var result = ListingValidation.ValidateCity(list);
            // Assert
            Assert.False(result);
        }


        [Fact]
        public void ValidateRoommateTrueCase()
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
        public void ValidateRoommateFalseCase()
        {
            // Arrange
            Listing list = new Listing { RoommateCount = 12 };
            ListingValidation ListingValidation = new ListingValidation();
            // Act
            var result = ListingValidation.ValidateRoommateCount(list);
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ValidatePhoneNumberTrueCase()
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
        public void ValidatePhoneNumberFalseCase()
        {
            // Arrange
            Listing list = new Listing { Phone = "+860026689" };
            ListingValidation ListingValidation = new ListingValidation();
            // Act
            var result = ListingValidation.ValidatePhoneNumber(list);
            // Assert
            Assert.False(result);
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
