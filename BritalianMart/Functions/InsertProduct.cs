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


        public InsertProduct(CosmosClient cosmosClient, IProductValidator validator ) {

            _cosmosClient = cosmosClient ?? throw new ArgumentException(nameof(cosmosClient));
            _cosmosDB = _cosmosClient.GetDatabase("ToDoList");
            _validator = validator;

        }

      

        [FunctionName("InsertProduct")]
        public  async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] ProductModel product, ILogger log)
         {
           
            try
            {

                if(_validator.IsValid(product) == true) {

                    product.Id = Guid.NewGuid().ToString();
                    product.Created = DateTime.UtcNow;
                    product.Modified = DateTime.UtcNow;

                    var dbContainer = _cosmosDB.GetContainer("ProductCatalog");
                    var response = await dbContainer.CreateItemAsync(product);
                    log.LogInformation($"Product Inserted into cosmoDB successfully");
                    return new CreatedResult(product.Id, product);

                }else
                {
                    log.LogInformation("Product Not validated");
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
