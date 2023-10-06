using AutoFixture;
using BritalianMart.Catalog.Interfaces;
using BritalianMart.Functions;
using BritalianMart.Models;
using BritalianMart.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BritalianMartTests.FunctionsTest
{
    public class InsertProductTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IProductCatalog> _mockCatalog;
        private readonly Mock<ILogger> _mockLogger;
        private readonly ProductValidator _validator;

        public InsertProductTest()
        {
            _fixture = new Fixture();
            _mockCatalog = new Mock<IProductCatalog>();
            _mockLogger = new Mock<ILogger>();
            _validator = new ProductValidator();


        }

        [Fact]
        public async Task InsertProduct_ValidProduct_returnsCreatedResult()
        {
            // Arrange
            var product = _fixture.Create<ProductModel>();

            _mockCatalog.Setup(x => x.Add(product)).Returns(Task.CompletedTask);

            var sut = new InsertProduct(_mockCatalog.Object, _validator);

            // Act
            var result = await sut.Run(product, _mockLogger.Object);

            // Assert
            _mockCatalog.Verify(x => x.Add(product), Times.Exactly(1));
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public async Task InsertProduct_InValidProduct_WithPluNotValid_returnsBadRequest()
        {
            // Arrange
            var product = _fixture.Create<ProductModel>();
            product.Plu = "";

            _mockCatalog.Setup(x => x.Add(product)).Returns(Task.CompletedTask);

            var sut = new InsertProduct(_mockCatalog.Object, _validator);

            // Act
            var result = await sut.Run(product, _mockLogger.Object);

            // Assert
            _mockCatalog.Verify(x => x.Add(product), Times.Exactly(0));
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task InsertProduct_InValidProduct_WithDescriptionNotValid_WhenDescriptionLengthisLessThan5_returnsBadRequest()
        {
            // Arrange

            var product = _fixture.Create<ProductModel>();
            product.Description = "123";

            _mockCatalog.Setup(x => x.Add(product)).Returns(Task.CompletedTask);

            var sut = new InsertProduct(_mockCatalog.Object, _validator);

            // Act
            var result = await sut.Run(product, _mockLogger.Object);

            // Assert
            _mockCatalog.Verify(x => x.Add(product), Times.Exactly(0));
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task InsertProduct_InValidProduct_WhenPriceIsNegative_returnsBadRequest()
        {
            // Arrange
            var product = _fixture.Create<ProductModel>();
            product.Price = -1;

            _mockCatalog.Setup(x => x.Add(product)).Returns(Task.CompletedTask);

            var sut = new InsertProduct(_mockCatalog.Object, _validator);
            // Act
            var result = await sut.Run(product, _mockLogger.Object);

            // Assert
            _mockCatalog.Verify(x => x.Add(product), Times.Exactly(0));
            result.Should().BeOfType<BadRequestObjectResult>();
        }




    }
}
