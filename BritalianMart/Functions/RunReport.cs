using BritalianMart.Reports.Interfaces;
using BritalianMart.Reports.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BritalianMart.Reports.Functions
{

    public class RunReport
    {
        private readonly IProductsReport _dbReport;


        public RunReport(IProductsReport dbReport) {
        
            _dbReport = dbReport;

        }
        [FunctionName("GetProductsReport")]
        public async Task<IActionResult> GetReport(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "report")] HttpRequest req,
        ILogger log)
        {

        
            var products =  _dbReport.GetReport();

            return new OkObjectResult(products);
        }
        //Want to create a report (pdf/xml format) which will give you back a document with:
        //  {"report_name: "catalog_report" } report name gonna be the name you want to be instead the catalog  is the name of the catalog i Have in my database that is called "ProductCatalog"
        // in this report I will have a description of the item and the price.

        // once I have this document i will need to store it into the blob storage and in the database called Reports


        // once I have this implementation in place I will need to do same steps but using queue


        //test1 run the GetProductsReport

    }
}
