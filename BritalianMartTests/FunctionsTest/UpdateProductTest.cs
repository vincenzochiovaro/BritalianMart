using AutoFixture;
using BritalianMart.Catalog.Interfaces;
using BritalianMart.Functions;
using BritalianMart.Models;
using BritalianMart.Services;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BritalianMartTests.FunctionsTest
{
    public class UpdateProductTest
    {
        private Mock<IProductCatalog> _mockCatalog;
        private AbstractValidator<ProductModel> _productValidator;
        private Mock<ILogger> _mockLogger;
        private Fixture _fixture;

        public UpdateProductTest()
        {
            _mockCatalog = new Mock<IProductCatalog>();
            _productValidator = new ProductValidator();
            _mockLogger = new Mock<ILogger>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task UpdateProduct_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var existingProduct = _fixture.Create<ProductModel>();
            existingProduct.Id = "1";

             var incomingProduct = _fixture.Build<ProductModel>()
                                          .With(x => x.Id, "1")
                                          .With(x => x.Description, "NewDescription")
                                          .Create();

            _mockCatalog.Setup(x => x.GetById("1")).ReturnsAsync(existingProduct);
            _mockCatalog.Setup(x => x.Update(existingProduct)).Returns(Task.CompletedTask);


            var sut = new UpdateProduct(_productValidator, _mockCatalog.Object);

            // Act
            var result = await sut.Run(incomingProduct, _mockLogger.Object, "1");

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _mockCatalog.Verify(x => x.GetById("1"), Times.Exactly(1));
            _mockCatalog.Verify(y => y.Update(existingProduct), Times.Exactly(1));
            existingProduct.Description.Should().Be("NewDescription");


        }
        [Fact]
        public async Task UpdateProduct_InValidInput_ReturnsBadRequest()
        {
            // Arrange
            var existingProduct = _fixture.Build<ProductModel>()
                                          .With(x => x.Id, "1")
                                          .Create();


            var incomingProduct = _fixture.Build<ProductModel>()
                                          .With(x => x.Id, "1")
                                          .With(x => x.Description, "bad")
                                          .Create();

            _mockCatalog.Setup(x => x.GetById("1")).ReturnsAsync(existingProduct);
            _mockCatalog.Setup(x => x.Update(existingProduct)).Returns(Task.CompletedTask);


            var sut = new UpdateProduct(_productValidator, _mockCatalog.Object);

            // Act
            var result = await sut.Run(incomingProduct, _mockLogger.Object, "1");

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _mockCatalog.Verify(x => x.GetById("1"), Times.Exactly(1));
            _mockCatalog.Verify(y => y.Update(existingProduct), Times.Exactly(0));



        }

    }
}


