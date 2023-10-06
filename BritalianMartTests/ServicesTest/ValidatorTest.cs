using AutoFixture;
using BritalianMart.Models;
using BritalianMart.Services;
using FluentAssertions;

namespace BritalianMartTests.ServicesTest
{
    public class ProductValidatorTests
    {
        private Fixture _fixture;


        public ProductValidatorTests() {
            _fixture = new Fixture();
        }
        [Fact]
        public void IsValid_ValidProduct_ReturnsTrue()
        {
            // Arrange
            var validator = new ProductValidator();
            var product = _fixture.Create<ProductModel>();

            // Act
            var result = validator.Validate(product);

            // Assert
            result.IsValid.Should().BeTrue();



        }
        [Fact]
        public void IsValid_DescriptionTooShort_ReturnsFalse()
        {
            // Arrange
            var validator = new ProductValidator();
            var product = _fixture.Build<ProductModel>()
                .With(x => x.Description, "123")
                .Create();


            // Act
            var result = validator.Validate(product);

            // Assert
            result.IsValid.Should().BeFalse();

        }


        [Fact]
        public void IsValid_PriceIsNegativeNumber_ReturnsFalse()
        {
            // Arrange
            var validator = new ProductValidator();
            var product = _fixture.Build<ProductModel>()
                 .With(x => x.Price, -1)
                 .Create();

            // Act
            var result = validator.Validate(product);


            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void IsValid_PluEmpty_ReturnsFalse()
        {
            // Arrange
            var validator = new ProductValidator();
            var product = _fixture.Build<ProductModel>()
                 .With(x => x.Plu, "")
                 .Create();

            // Act
            var result = validator.Validate(product);

            // Assert
            result.IsValid.Should().BeFalse();
        }



    }
}
