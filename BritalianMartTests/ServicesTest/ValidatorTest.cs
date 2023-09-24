using Xunit;
using BritalianMart.Models;
using BritalianMart.Interfaces;
using BritalianMart.Services;
using FluentAssertions;

namespace BritalianMartTests.ServicesTest
{
    public class ProductValidatorTests
    {
        [Fact]
        public void IsValid_ValidProduct_ReturnsTrue()
        {
            // Arrange
            var validator = new ProductValidator();
            var product = new ProductModel
            {
                Plu = "validPlu",
                Description = "ValidDescription",
                Price = "10.00"
            };

            // Act
            bool result = validator.IsValid(product);

            // Assert
            result.Should().BeTrue();
        
        }
        [Fact]
        public void IsValid_DescriptionTooShort_ReturnsFalse()
        {
            // Arrange
            var validator = new ProductValidator();
            var product = new ProductModel
            {
                Plu = "validPlu",
                Description = "Shrt",
                Price = "10.00"
            };

            // Act
            bool result = validator.IsValid(product);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsValid_PriceNotParseable_ReturnsFalse()
        {
            // Arrange
            var validator = new ProductValidator();
            var product = new ProductModel
            {
                Plu = "validPlu",
                Description = "ValidDescription",
                Price = "NotAPrice" 
            };

            // Act
            bool result = validator.IsValid(product);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsValid_PriceLessThanOrEqualToZero_ReturnsFalse()
        {
            // Arrange
            var validator = new ProductValidator();
            var product = new ProductModel
            {
                Plu = "validPlu",
                Description = "ValidDescription",
                Price = "0.00" 
            };

            // Act
            bool result = validator.IsValid(product);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsValid_PluEmpty_ReturnsFalse()
        {
            // Arrange
            var validator = new ProductValidator();
            var product = new ProductModel
            {
                Plu = "", 
                Description = "ValidDescription",
                Price = "10.00"
            };

            // Act
            bool result = validator.IsValid(product);

            // Assert
            result.Should().BeFalse();
        }



    }
}
