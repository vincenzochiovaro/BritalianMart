using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;
using BritalianMart.Models;
using Azure;
using BritalianMart.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BritalianMart.Functions
{
    


    public  class InsertProduct
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Database _cosmosDB;
        private readonly IProductValidator _validator;

        //ASK Andy how can I understand what is validator? because if f12 doesn't bring me to the actual implementation. So far the only way i have is to go to the startup.cs file and see the injection

        public InsertProduct(CosmosClient cosmosClient, IProductValidator validator ) {

            _cosmosClient = cosmosClient ?? throw new ArgumentException(nameof(cosmosClient));
            _cosmosDB = _cosmosClient.GetDatabase("ToDoList");
            _validator = validator;

        }

      

        [FunctionName("InsertProduct")]
        public  async Task<IActionResult> Run(
            //NOTE: the param product is a kind of shortcut to avoid few lines of code
            //1: when exectued the function we'll have a product already with the information passed with postman
            //2: without this process we should have  req, deserialize the data and insert that into the db
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] ProductModel product, ILogger log)
         {

            log.LogInformation($"product to insert into db is: {product}");
           
            try
            {

                if(_validator.IsValid(product) == true) {

                    product.Id = Guid.NewGuid().ToString();
                    product.Created = DateTime.UtcNow;
                    product.Modified = DateTime.UtcNow;

                    var dbContainer = _cosmosDB.GetContainer("ProductCatalog");
                    var response = await dbContainer.CreateItemAsync(product);

                    return new CreatedResult(product.Id, product);

                }else
                {
                    log.LogInformation("Not validated");
                    //This is the same as node.js (next) where we pass the exception trough catch blocks.
                    throw new Exception("Product validation failed.");
                }
               
            }
            catch (CosmosException cosmosEx)
            {
                log.LogError($"Cosmos DB Error: Status code {cosmosEx.StatusCode}, Message: {cosmosEx.Message}");
                return new BadRequestObjectResult(cosmosEx);
            }
            catch (Exception ex)
            {
                log.LogError($"Error inserting document: {ex}");
                return new BadRequestObjectResult(ex);
            }

        }
    }
}
