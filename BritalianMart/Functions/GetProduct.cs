using BritalianMart.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
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

        [FunctionName("GetAllProductsFromCosmosDb")]

        public async Task<IActionResult> GetAllProductsCosmos(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cosmos/allProducts")] HttpRequest req,
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


    }
}

