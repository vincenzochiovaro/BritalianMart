
using BritalianMart.Functions;
using BritalianMart.Interfaces;
using BritalianMart.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using AutoFixture;
namespace BritalianMartTests.FunctionsTest
{
    public class GetProductTest
    {
        private Fixture _fixture;
        private Mock<IProductCatalog> _mockCatalogDatabase;

        public GetProductTest() { 
            _fixture = new Fixture();
            _mockCatalogDatabase = new Mock<IProductCatalog>();

        }

        [Fact]
        public async Task GetProduct_Success_ReturnOkObjectResult_Cosmos()
        {
            // Arrange
            var productModels = _fixture.CreateMany<ProductModel>(2).ToList();
            _mockCatalogDatabase.Setup(x => x.GetAll()).ReturnsAsync(productModels);

            var sut = new GetProduct(_mockCatalogDatabase.Object);

            // Act
            var result = await sut.GetAllProductsCosmos(null,null);

            // Assert

            _mockCatalogDatabase.Verify(x => x.GetAll(), Times.Once());
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetProduct_Exception_ReturnsBadRequestObjectResult_Cosmos()
        {
            // Arrange
            _mockCatalogDatabase.Setup(x => x.GetAll()).ThrowsAsync(new Exception("Some error message"));

            var sut = new GetProduct(_mockCatalogDatabase.Object);

            // Act
            var result = await sut.GetAllProductsCosmos(null, null);

            // Assert
            _mockCatalogDatabase.Verify(x => x.GetAll(), Times.Once());
            result.Should().BeOfType<BadRequestObjectResult>();
            
        }


    }




}




