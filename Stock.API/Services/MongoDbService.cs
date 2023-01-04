using MongoDB.Driver;

namespace Stock.API.Services
{
    public class MongoDbService
    {
        readonly IMongoDatabase _database;
        public MongoDbService()
        {
            MongoClient client = new("mongodb://localhost:27017");
            _database = client.GetDatabase("SagaChoreographyDB");
        }
        public IMongoCollection<T> GetCollection<T>() => _database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }
}
