using BritalianMart.Interfaces;
using BritalianMart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace BritalianMart.Functions
{



    public  class InsertProduct
    {
       
        private readonly IProductValidator _validator;
        private readonly IProductCatalog _catalog;

        public InsertProduct(IProductCatalog catalog, IProductValidator validator ) {

           _catalog = catalog;
           _validator = validator;

        }



        [FunctionName("InsertProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "product")] ProductModel product, ILogger log)
         {
           
            try
            {

                if(_validator.IsValid(product) == true) {

                    product.Id = Guid.NewGuid().ToString();
                    product.Created = DateTime.UtcNow;
                    product.Modified = DateTime.UtcNow;



                    await _catalog.Add(product);

                    log.LogInformation($"Product Inserted into DB successfully");
                    return new CreatedResult(product.Id,product);
                   
                    

                }else
                {
                    log.LogInformation("Product Not validated in InsertProductCosmos");
                    return new BadRequestObjectResult(product); 
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