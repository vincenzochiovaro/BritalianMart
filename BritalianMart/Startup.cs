using BritalianMart;
using BritalianMart.Catalog.Interfaces;
using BritalianMart.Models;
using BritalianMart.Reports.Functions;
using BritalianMart.Reports.Interfaces;
using BritalianMart.Reports.Services;
using BritalianMart.Services;
using FluentValidation;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
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

            var cosmoClient = new CosmosClient(Environment.GetEnvironmentVariable("CosmosDb:ConnectionString"), options);
            builder.Services.AddSingleton(cosmoClient);

            builder.Services.AddScoped<AbstractValidator<ProductModel>, ProductValidator>();
            builder.Services.AddScoped<IProductCatalog, CosmosDatabase>();
            builder.Services.AddScoped<IProductsReport, CosmosDbReports>();
        }
    } }
