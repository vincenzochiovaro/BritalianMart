using BritalianMart.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace BritalianMart.Functions
{
    public class mongoStuff
    {
        private readonly MongoClient _mongoClient;

        public mongoStuff(MongoClient mongoClient)
        {
            _mongoClient = mongoClient ?? throw new ArgumentException(nameof(mongoClient));
        }

        [FunctionName("mongoStuff")]
        public async Task<IActionResult> RunMongo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "mongoStuff/{brand}")] HttpRequest req,
            ILogger log, string brand)
        {
            try
            {
                var database = _mongoClient.GetDatabase("BritalianMartDB");
                var collection = database.GetCollection<ProductModel>("ProductCatalog");

                var filter = Builders<ProductModel>.Filter.Eq("Brand", $"{brand}");
                var products = await collection.Find(filter).ToListAsync();
              
                return new OkObjectResult(products);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error retrieving data from MongoDB.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
