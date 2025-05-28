using MongoDB.Driver;

namespace Strong_eCourses.Services
{
    public class MongoDBService
    {
        private readonly IMongoDatabase _database;

        public MongoDBService(IMongoClient client, IConfiguration configuration)
        {
            var databaseName = configuration["MongoDbConfig:Name"];
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}