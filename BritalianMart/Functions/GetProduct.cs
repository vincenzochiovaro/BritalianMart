using BritalianMart.Catalog.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BritalianMart.Functions
{

    public class GetProduct
    {
        private readonly IProductCatalog _catalog;


        public GetProduct(IProductCatalog catalog)
        {
            _catalog = catalog;
        }

        [FunctionName("GetAllProducts")]

        public async Task<IActionResult> GetAllProducts(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "allProducts")] HttpRequest req,
        ILogger log)
        {
            try
            {
                var products = await _catalog.GetAll();

                return new OkObjectResult(products);

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }


        }
        [FunctionName("GetProductById")]

        public async Task<IActionResult> GetProductById(
     [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product/{id}")] HttpRequest req,
           ILogger log, string id)
        {
            try
            {
                var product = await _catalog.GetById(id);

                return new OkObjectResult(product);

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }


        }


    }
}

