using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Queues;
using BritalianMart.Reports.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BritalianMart.Reports.Functions
{
    public class RunReport
    {
        private readonly IProductsReport _productsReport;
        private readonly QueueServiceClient _queueServiceClient;
        private readonly BlobServiceClient _blobServiceClient;

        public RunReport(IProductsReport productsReport, BlobServiceClient blobServiceClient, QueueServiceClient queueServiceClient)
        {
            _productsReport = productsReport ?? throw new ArgumentNullException(nameof(productsReport));
            _blobServiceClient = blobServiceClient ?? throw new ArgumentNullException(nameof(blobServiceClient));
            _queueServiceClient = queueServiceClient ?? throw new ArgumentNullException(nameof(queueServiceClient));
        }

        [FunctionName("GetProductsReport")]
        public async Task<IActionResult> GetReport(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "report")] HttpRequest req,
            ILogger log)
        {
            try
            {
                var randomReportPrefix = Guid.NewGuid().ToString();
                var listOfProducts = await _productsReport.GetReport();

                Task myParallelTask = Task.Run(async () =>
                {
                    try
                    {
                        var serializedProducts = JsonSerializer.Serialize(listOfProducts);
                        var queueClient = _queueServiceClient.GetQueueClient("britalianqueue");
                        await queueClient.SendMessageAsync(serializedProducts);

                        var blobContainer = _blobServiceClient.GetBlobContainerClient("britalianblob");
                        var blobName = GenerateBlobName(randomReportPrefix);
                        var blockBlob = blobContainer.GetBlockBlobClient(blobName);

                        await Task.Delay(5000);
                        var receivedMessage = await queueClient.ReceiveMessageAsync();

                        if (receivedMessage != null)
                        {
                            var messageContent = receivedMessage.Value.Body.ToString();

                            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(messageContent)))
                            {
                                await blockBlob.UploadAsync(stream);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                });

                return new OkObjectResult(listOfProducts);
            }
            catch (Exception ex)
            {
                log.LogError($"An error occurred: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        private string GenerateBlobName(string randomReportPrefix)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddTHHmmss");
            return $"productsReport_{timestamp}_{randomReportPrefix}.json";
        }
    }
}
