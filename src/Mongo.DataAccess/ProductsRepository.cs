namespace Mongo.DataAccess
{
    using Domain;
    using MongoDB.Driver;
    using System.Threading.Tasks;

    public class ProductsRepository
    {
        private readonly IMongoDatabase _database;

        private IMongoCollection<Product> Products => _database.GetCollection<Product>("products");

        public ProductsRepository(
            string connectionString,
            string databaseName)
        {
            IMongoClient client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public async Task AddProducts(Product[] products)
        {
            await Products.InsertManyAsync(products);
        }
    }
}
