using BritalianMart.Models;
using BritalianMart.Reports.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace BritalianMart.Reports.Services
{
    public class CosmosDbReports : IProductsReport
    {
        private readonly CosmosClient _cosmosClient;

        public CosmosDbReports(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task GetReport()
        {
            var cosmosDatabase = _cosmosClient.GetDatabase("BritalianMartDB");
            var container = cosmosDatabase.GetContainer("ProductCatalog");
            var products = new List<ProductModel>();

            var resultSet = container.GetItemLinqQueryable<ProductModel>().ToFeedIterator();
            while (resultSet.HasMoreResults)
            {
                var result = await resultSet.ReadNextAsync();
                products.AddRange(result);
            }

    // Resolve "loop" error
    // Maybe try to create a different model and replace te ProductModel with ReportModel
        }

     
    }
}
