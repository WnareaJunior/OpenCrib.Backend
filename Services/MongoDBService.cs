using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OpenCrib.api.Models;
using MongoDB.Bson;

namespace OpenCrib.api.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<User> _userCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _userCollection = database.GetCollection<User>(mongoDBSettings.Value.CollectionName);

        }
        public async Task CreateAsync(User user)
        {
            await _userCollection.InsertOneAsync(user);
            return;
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _userCollection.Find(new BsonDocument()).ToListAsync();
            
        }
        public async Task DeleteAsync(string id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("id", id);
            await _userCollection.DeleteOneAsync(filter);
            return;
        }

    }
}
