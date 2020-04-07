namespace MongoFunction
{
    using Domain;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Mongo.DataAccess;
    using Newtonsoft.Json;
    using System.IO;
    using System.Threading.Tasks;

    public static class ProductFunction
    {
        [FunctionName("AddProduct")]
        public static async Task<ActionResult<string>> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request)
        {
            var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            var product = JsonConvert.DeserializeObject<Product>(requestBody);

            // Hard-coded Connection String & Database name, as they are not the point of focus for this exercise
            var connectionString = "<Replace with your connection string>";
            var databaseName = "GroceryStore";

            var repository = new ProductsRepository(
                connectionString,
                databaseName);

            await repository.AddProduct(product);

            return new OkObjectResult("Product added successfully");
        }
    }
}
