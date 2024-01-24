using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using BritalianMart.Models;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos.Linq;
using BritalianMart.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BritalianMart.Services
{
    public class CosmosDatabase : IProductCatalog
    {
        private readonly CosmosClient _cosmosClient;

        public CosmosDatabase(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task Add(ProductModel product) //how product travelled here? 
        {
            var dbConnection = _cosmosClient.GetDatabase("BritalianMartDB");
            var dbContainer = dbConnection.GetContainer("ProductCatalog");

            await dbContainer.CreateItemAsync(product);

        }

        public async Task<IEnumerable<ProductModel>> GetAll()
        {
            var cosmosDatabase = _cosmosClient.GetDatabase("BritalianMartDB");
            var container = cosmosDatabase.GetContainer("ProductCatalog");

            var products = new List<ProductModel>();

            var resultSet = container.GetItemLinqQueryable<ProductModel>()
                                     .ToFeedIterator();


            while (resultSet.HasMoreResults == true)
            {
                products.AddRange(await resultSet.ReadNextAsync());
            }

            return products;
        }

        public Task<ProductModel> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(ProductModel productModel)
        {
            throw new NotImplementedException();
        }

        
    }
}

