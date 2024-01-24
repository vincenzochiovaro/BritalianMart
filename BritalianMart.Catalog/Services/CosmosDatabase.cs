using BritalianMart.Catalog.Interfaces;
using BritalianMart.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace BritalianMart.Services
{
    public class CosmosDatabase : IProductCatalog
    {
        private readonly CosmosClient _cosmosClient;

        public CosmosDatabase(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task Add(ProductModel product)
        {
            var dbConnection = _cosmosClient.GetDatabase("BritalianMartDB");
            var dbContainer = dbConnection.GetContainer("ProductCatalog");

            await dbContainer.CreateItemAsync(product);

        }

        public async Task<IEnumerable<ProductModel>> GetAll()
        {
            var cosmosDatabase = _cosmosClient.GetDatabase("BritalianMartDB");
            var container = cosmosDatabase.GetContainer("ProductCatalog");

            var products = new List<ProductModel>();

            var resultSet = container.GetItemLinqQueryable<ProductModel>()
                                     .ToFeedIterator();


            while (resultSet.HasMoreResults == true)
            {
                products.AddRange(await resultSet.ReadNextAsync());
            }

            return products;
        }

        public async Task<ProductModel> GetById(string id)
        {
            var cosmosDatabase = _cosmosClient.GetDatabase("BritalianMartDB");
            var container = cosmosDatabase.GetContainer("ProductCatalog");

            var products = new List<ProductModel>();



            var resultSet = container.GetItemLinqQueryable<ProductModel>()
                              .Where(p => p.Id == id)
                              .ToFeedIterator();

            var product = (await resultSet.ReadNextAsync()).FirstOrDefault();

            return product!;



        }

        public async Task Update(ProductModel productModel)

        {
            var cosmosDatabase = _cosmosClient.GetDatabase("BritalianMartDB");
            var container = cosmosDatabase.GetContainer("ProductCatalog");


            await container.ReplaceItemAsync(
              productModel,
              productModel.Id);

        }


    }
}

