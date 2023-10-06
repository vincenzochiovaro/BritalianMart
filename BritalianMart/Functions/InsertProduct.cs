using BritalianMart.Catalog.Interfaces;
using BritalianMart.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BritalianMart.Functions
{



    public  class InsertProduct 
    {
       
        private readonly AbstractValidator<ProductModel> _validator;
        private readonly IProductCatalog _catalog;

        public InsertProduct(IProductCatalog catalog, AbstractValidator<ProductModel> validator ) {

           _catalog = catalog;
           _validator = validator;

        }



        [FunctionName("InsertProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "product")] ProductModel product, ILogger log)
         {
           
            try
            {

                var validatedProduct = _validator.Validate(product);

                if(_validator.Validate(product).IsValid) {

                    product.Id = Guid.NewGuid().ToString();
                    product.Created = DateTime.UtcNow;
                    product.Modified = DateTime.UtcNow;

                    // referencing to Andy video, we should make an extra check to prevent "duplicates"
                    // BUTTT what if I want to insert 2 pints of milk with same plu and price and description?
                    // if I referencethe Id (Guid) it will always be a "different" item so will be inserted regard if is a duplicated or an item to add.
                  
                   
                    //Solution is: If We have a "duplicate" Item, we send back a message to the user says: Hey we have already 1 pint of milk are you sure you want to have a duplicate?
                    
                    

                    await _catalog.Add(product);

                    log.LogInformation($"Product Inserted into DB successfully");
                    return new CreatedResult(product.Id,product);
                   
                    

                }else
                {
                    log.LogWarning(validatedProduct.ToString());
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