namespace MongoFunction
{
    using Domain;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.Storage;
    using Microsoft.Azure.Storage.Blob;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System.Linq;
    using System.Threading.Tasks;

    public static class BlobToMongoFunction
    {
        [FunctionName("BlobToMongoFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            // Hard-coded Connection String & Database name, as they are not the point of focus for this exercise
            var blobConnectionString = "DefaultEndpointsProtocol=https;AccountName=mongosstore;AccountKey=YCvla7l+xMpIdl3CFjZ+hzMr6PLd9zxwezQIhy97dSQT7hjsx9UxRH3hQGh5YpRWEZ8nNlRW+wCChrjJ7u6elQ==;EndpointSuffix=core.windows.net";
            var content = CloudStorageAccount
                .Parse(blobConnectionString)
                .CreateCloudBlobClient()
                .GetContainerReference("mongoblob")
                .GetBlockBlobReference("blob.json")
                .DownloadText();

            log.LogInformation(content);

            var products = JsonConvert.DeserializeObject<Product[]>(content).ToArray();
            var result = await Helper.WriteToMongo(products);
            var message = result ? "Products added successfully" : "No products were added";
            return new OkObjectResult(message);
        }
    }
}
