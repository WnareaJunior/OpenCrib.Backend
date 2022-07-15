using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OpenCrib.api.Models;
using MongoDB.Bson;


namespace OpenCrib.api.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<User> _userCollection;
        private readonly IMongoCollection<Party> _partyCollection;
        private readonly IMongoCollection<ZipCode> _zipCodeCollection;
        private readonly List<ZipCode> _zipList;
        private readonly List<String> _zipStringList = new List<string>();

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _userCollection = database.GetCollection<User>(mongoDBSettings.Value.UserCollection);
            _partyCollection = database.GetCollection<Party>(mongoDBSettings.Value.PartyCollection);
            _zipCodeCollection = database.GetCollection<ZipCode>(mongoDBSettings.Value.ZipCodeCollection);
            _zipList = _zipCodeCollection.Aggregate().ToList();
            foreach (ZipCode item in _zipList)
            {
                _zipStringList.Add(item.get());
            }
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

        //GetNearbyParties returns post because all posts are about parties(duh)
        public async Task<List<Party>> PartiesInsideZip(string postalCode)
        {
           

            return await _partyCollection.Find(x => x.Address.PostalCode == postalCode).ToListAsync();

            
           
        }

        public async Task<List<Party>> PartiesNearZip(string zipCode,int range)
        {
            int index = _zipStringList.IndexOf(zipCode);
            
            var partiesNearby = await _partyCollection.Find(x => x.Address.PostalCode == _zipList[index].Zip).ToListAsync();

            for (int i = index; i < _zipList.Count&& i <index+range; i++)
            {
                partiesNearby.AddRange(await _partyCollection.Find(x => x.Address.PostalCode == _zipList[i].Zip).ToListAsync());
            }
            for (int i = index; i >= 0 && i > index - range; i--)
            {
                partiesNearby.AddRange(await _partyCollection.Find(x => x.Address.PostalCode == _zipList[i].Zip).ToListAsync());
            }


            return partiesNearby;




        }


        public async Task DeleteAsync(string id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("id", id);
            await _userCollection.DeleteOneAsync(filter);
            return;
        }

    }
}
