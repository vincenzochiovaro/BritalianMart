using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BritalianMart.Functions;
using BritalianMart.Interfaces;
using BritalianMart.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Moq;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using BritalianMart.Services;

namespace BritalianMartTests.FunctionsTest
{
    public class InsertProductTest
    {
        [Fact]
        public async Task InsertProduct_ValidProduct_returnsCreatedResult()
        {
            //Arrange
            var loggerMock = new Mock<ILogger>();
            var cosmosClientMock = new Mock<CosmosClient>();

            //Mock all the steps of cosmosdb
            cosmosClientMock.Setup(service => service.GetDatabase("ToDoList")).Returns(() =>
            {
                var databaseMock = new Mock<Database>();
                databaseMock.Setup(service => service.GetContainer("ProductCatalog"))
                .Returns(() =>
                {
                    var containerMock = new Mock<Container>();
                    var cosmosResponseMock = new Mock<ItemResponse<ProductModel>>();
                    //setup containerMock
                    containerMock.Setup(service => service.CreateItemAsync(It.IsAny<ProductModel>(), null, null, default)).ReturnsAsync(cosmosResponseMock.Object);
                    return containerMock.Object;
                });
                return databaseMock.Object;
               
            });

            var product = new ProductModel
            {
                Plu = "validPlu",
                Description = "ValidDescription",
                Price = "0.01"
            };


            //Should I mock the validator?
            var validator = new ProductValidator();
            var sut = new InsertProduct(cosmosClientMock.Object, validator);

            //Act
            var result = await sut.Run(product, loggerMock.Object);

            //Assert
            result.Should().BeOfType<CreatedResult>();
        }
        [Fact]
        public async Task InsertProduct_InValidProduct_WithPluNotValid_returnsBadRequest()
        {
            //Arrange
            var loggerMock = new Mock<ILogger>();
            var cosmosClientMock = new Mock<CosmosClient>();

            //Mock all the steps of cosmosdb
            cosmosClientMock.Setup(service => service.GetDatabase("ToDoList")).Returns(() =>
            {
                var databaseMock = new Mock<Database>();
                databaseMock.Setup(service => service.GetContainer("ProductCatalog"))
                .Returns(() =>
                {
                    var containerMock = new Mock<Container>();
                    var cosmosResponseMock = new Mock<ItemResponse<ProductModel>>();
                    //setup containerMock
                    containerMock.Setup(service => service.CreateItemAsync(It.IsAny<ProductModel>(), null, null, default)).ReturnsAsync(cosmosResponseMock.Object);
                    return containerMock.Object;
                });
                return databaseMock.Object;

            });

            var product = new ProductModel
            {
                Plu = "",
                Description = "ValidDescription",
                Price = "0.01"
            };

            //Should I mock the validator?
            var validator = new ProductValidator();
            

            var sut = new InsertProduct(cosmosClientMock.Object, validator);

           

            //Act
            var result = await sut.Run(product, loggerMock.Object);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Fact]
        public async Task InsertProduct_InValidProduct_WithDescriptionNotVali_WhenDescriptionLengthisLessThan5_returnsBadRequest()
        {
            //Arrange
            var loggerMock = new Mock<ILogger>();
            var cosmosClientMock = new Mock<CosmosClient>();

            //Mock all the steps of cosmosdb
            cosmosClientMock.Setup(service => service.GetDatabase("ToDoList")).Returns(() =>
            {
                var databaseMock = new Mock<Database>();
                databaseMock.Setup(service => service.GetContainer("ProductCatalog"))
                .Returns(() =>
                {
                    var containerMock = new Mock<Container>();
                    var cosmosResponseMock = new Mock<ItemResponse<ProductModel>>();
                    //setup containerMock
                    containerMock.Setup(service => service.CreateItemAsync(It.IsAny<ProductModel>(), null, null, default)).ReturnsAsync(cosmosResponseMock.Object);
                    return containerMock.Object;
                });
                return databaseMock.Object;

            });

            var product = new ProductModel
            {
                Plu = "validPlu",
                Description = "1234",
                Price = "0.01"
            };

            //Should I mock the validator?
            var validator = new ProductValidator();


            var sut = new InsertProduct(cosmosClientMock.Object, validator);



            //Act
            var result = await sut.Run(product, loggerMock.Object);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Fact]
        public async Task InsertProduct_InValidProduct_WhenPriceIsLessThan001_returnsBadRequest()
        {
            //Arrange
            var loggerMock = new Mock<ILogger>();
            var cosmosClientMock = new Mock<CosmosClient>();

            //Mock all the steps of cosmosdb
            cosmosClientMock.Setup(service => service.GetDatabase("ToDoList")).Returns(() =>
            {
                var databaseMock = new Mock<Database>();
                databaseMock.Setup(service => service.GetContainer("ProductCatalog"))
                .Returns(() =>
                {
                    var containerMock = new Mock<Container>();
                    var cosmosResponseMock = new Mock<ItemResponse<ProductModel>>();
                    //setup containerMock
                    containerMock.Setup(service => service.CreateItemAsync(It.IsAny<ProductModel>(), null, null, default)).ReturnsAsync(cosmosResponseMock.Object);
                    return containerMock.Object;
                });
                return databaseMock.Object;

            });

            var product = new ProductModel
            {
                Plu = "validPlu",
                Description = "validDescription",
                Price = "0.00"
            };

            //Should I mock the validator?
            var validator = new ProductValidator();


            var sut = new InsertProduct(cosmosClientMock.Object, validator);



            //Act
            var result = await sut.Run(product, loggerMock.Object);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Fact]
        public async Task InsertProduct_InValidProduct_WhenPriceIsStringDifferentThanFree_returnsBadRequest()
        {
            //Arrange
            var loggerMock = new Mock<ILogger>();
            var cosmosClientMock = new Mock<CosmosClient>();

            //Mock all the steps of cosmosdb
            cosmosClientMock.Setup(service => service.GetDatabase("ToDoList")).Returns(() =>
            {
                var databaseMock = new Mock<Database>();
                databaseMock.Setup(service => service.GetContainer("ProductCatalog"))
                .Returns(() =>
                {
                    var containerMock = new Mock<Container>();
                    var cosmosResponseMock = new Mock<ItemResponse<ProductModel>>();
                    //setup containerMock
                    containerMock.Setup(service => service.CreateItemAsync(It.IsAny<ProductModel>(), null, null, default)).ReturnsAsync(cosmosResponseMock.Object);
                    return containerMock.Object;
                });
                return databaseMock.Object;

            });

            var product = new ProductModel
            {
                Plu = "validPlu",
                Description = "validDescription",
                Price = "can Be Free or Number"
            };

            //Should I mock the validator?
            var validator = new ProductValidator();


            var sut = new InsertProduct(cosmosClientMock.Object, validator);



            //Act
            var result = await sut.Run(product, loggerMock.Object);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Fact]
        public async Task InsertProduct_ValidProduct_WhenPriceIsSetToFree_returnsCreatedResult()
        {
            //Arrange
            var loggerMock = new Mock<ILogger>();
            var cosmosClientMock = new Mock<CosmosClient>();

            //Mock all the steps of cosmosdb
            cosmosClientMock.Setup(service => service.GetDatabase("ToDoList")).Returns(() =>
            {
                var databaseMock = new Mock<Database>();
                databaseMock.Setup(service => service.GetContainer("ProductCatalog"))
                .Returns(() =>
                {
                    var containerMock = new Mock<Container>();
                    var cosmosResponseMock = new Mock<ItemResponse<ProductModel>>();
                    //setup containerMock
                    containerMock.Setup(service => service.CreateItemAsync(It.IsAny<ProductModel>(), null, null, default)).ReturnsAsync(cosmosResponseMock.Object);
                    return containerMock.Object;
                });
                return databaseMock.Object;

            });

            var product = new ProductModel
            {
                Plu = "validPlu",
                Description = "validDescription",
                Price = "free"
            };

            //Should I mock the validator?
            var validator = new ProductValidator();


            var sut = new InsertProduct(cosmosClientMock.Object, validator);



            //Act
            var result = await sut.Run(product, loggerMock.Object);

            //Assert
            result.Should().BeOfType<CreatedResult>();
        }

    }
}
