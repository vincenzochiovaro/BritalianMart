using BritalianMart.Catalog.Interfaces;
using BritalianMart.Models;
using MongoDB.Driver;

namespace BritalianMart.Services
{
    public class MongoDatabase : IProductCatalog
    {
        private readonly MongoClient _mongoClient;

        public MongoDatabase(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }
        public async Task<IEnumerable<ProductModel>>  GetAll() 
        {
            var dbConnection = _mongoClient.GetDatabase("BritalianMartDB");
            var collection = dbConnection.GetCollection<ProductModel>("ProductCatalog");

            var filter = Builders<ProductModel>.Filter.Empty;

            var documents = await collection.Find(filter).ToListAsync();

            return documents;

        }
        public async Task Add(ProductModel productModel)
        {
            var dbConnection = _mongoClient.GetDatabase("BritalianMartDB");
            var collection = dbConnection.GetCollection<ProductModel>("ProductCatalog");

            
            var filter = Builders<ProductModel>.Filter.Eq(x => x.Plu, productModel.Plu);

            var existingDocument = await collection.Find(filter).FirstOrDefaultAsync();

            if (existingDocument != null)
            {
                Console.WriteLine("This Item Already exists in the db - Item not Added");
            }
            else
            {
                // Insert the document only if it doesn't already exist
                await collection.InsertOneAsync(productModel);
            }






        }

        public async Task Update(ProductModel productModel)

        {
            var dbConnection = _mongoClient.GetDatabase("BritalianMartDB");
            var collection = dbConnection.GetCollection<ProductModel>("ProductCatalog");

            var filter = Builders<ProductModel>.Filter.Eq("Id", productModel.Id);

            
            await collection.ReplaceOneAsync(filter, productModel);

        }

        public async Task<ProductModel> GetById(string id)
        {
            var dbConnection = _mongoClient.GetDatabase("BritalianMartDB");
            var collection = dbConnection.GetCollection<ProductModel>("ProductCatalog");

            var filter = Builders<ProductModel>.Filter.Eq("Id", id);

            var document = await collection.Find(filter).FirstOrDefaultAsync();

            return document;


        }


    }
}
