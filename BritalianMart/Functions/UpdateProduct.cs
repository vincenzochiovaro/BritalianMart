//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using Microsoft.Azure.Cosmos;
//using MongoDB.Driver;
//using BritalianMart.Interfaces;
//using BritalianMart.Models;
//using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;

//namespace BritalianMart.Functions
//{
//    public  class UpdateProduct
//    {
//        private readonly CosmosClient _cosmosClient;
//        private readonly MongoClient _mongoClient;
//        private readonly IProductValidator _productValidator;

//        public UpdateProduct(CosmosClient cosmosClient, MongoClient mongoClient, IProductValidator productValidator) {
//            _cosmosClient = cosmosClient;
//            _mongoClient = mongoClient;
//            _productValidator = productValidator;
//            //think about validator, what if we update description to be empty? 
//        }


//        [FunctionName("UpdateCosmosProduc")]
//        public  async Task<IActionResult> Run(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "cosmos/product/{id}")] HttpRequest req,
//            ILogger log, string id)
//        {

//            var cosmosDB = _cosmosClient.GetDatabase("BritalianMartDB");
//            var cosmosContainer = cosmosDB.GetContainer("ProductCatalog");
            


//            // I need to connect with cosmos and grab the document that match that id
//            // then I want to modify the specific field i am passing with postman

//            //example i have a product with id,description, price
//            // i send on postman that i want the description to be "hello world"


//            return new OkObjectResult("hi");

//        }
//    }
//}
