using BritalianMart;
using BritalianMart.Interfaces;
using BritalianMart.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;

[assembly: FunctionsStartup(typeof(Startup))]


namespace BritalianMart
{
    internal class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            CosmosClientOptions options = new()
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            };

            var cosmoClient = new CosmosClient(Environment.GetEnvironmentVariable("CosmosEmulatorString"), options);
            var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("MongoDb:ConnectionString"));
            builder.Services.AddSingleton(cosmoClient);
            builder.Services.AddSingleton(mongoClient);

            //Inject validator
            builder.Services.AddScoped<IProductValidator, ProductValidator>();
            builder.Services.AddScoped<IProductCatalog, CosmosDatabase>();
        }
    } }
