using BritalianMart.Models;
using BritalianMart.Reports.Interfaces;
using Microsoft.Azure.Cosmos;

namespace BritalianMart.Reports.Services
{
    public class CosmosDbReports : IProductsReport
    {
        private readonly CosmosClient _cosmosClient;

        public CosmosDbReports(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<List<ProductModel>> GetReport()
        {
            try
            {
                var cosmosDatabase = _cosmosClient.GetDatabase("BritalianMartDB");
                var container = cosmosDatabase.GetContainer("ProductCatalog");
                var query = new QueryDefinition(
                    query: "SELECT * FROM p"
                    );
                var products = new List<ProductModel>();

                using FeedIterator<ProductModel> feed = container.GetItemQueryIterator<ProductModel>(queryDefinition: query);

                while (feed.HasMoreResults)
                {
                    var response = await feed.ReadNextAsync();
                    foreach (var item in response)
                    {
                        products.Add(item);
                    }
                }

                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<ProductModel>();
            }

        }

    }
}
