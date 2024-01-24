using BritalianMart.Catalog.Interfaces;
using BritalianMart.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BritalianMart.Functions
{
    public class UpdateProduct
    {
        private readonly AbstractValidator<ProductModel> _productValidator;
        private readonly IProductCatalog _catalog;


        public UpdateProduct(AbstractValidator<ProductModel> productValidator, IProductCatalog catalog)
        {
            _productValidator = productValidator;
            _catalog = catalog;

        }

        [FunctionName("UpdateDescriptionProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "product/{id}")] ProductModel IncomingProduct,
            ILogger log, string id)
        {

            try
            {
                var existingProduct = await _catalog.GetById(id);

                existingProduct.Description = IncomingProduct.Description;

                if (_productValidator.Validate(existingProduct).IsValid)
                {
                    existingProduct.Modified = DateTime.UtcNow;

                    await _catalog.Update(existingProduct);
                    return new OkObjectResult(existingProduct);

                }
                else
                {
                    return new BadRequestObjectResult("Product can't be validated");
                }
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }


        }
    }
}
