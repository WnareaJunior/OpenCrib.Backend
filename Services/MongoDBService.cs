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

        public async void CreateRsvp(string myUserId, string partyId)
        {
            var partyFilter = Builders<Party>.Filter.Eq(x => x.Id, partyId);
            var partyCursor = await _partyCollection.FindAsync<Party>(partyFilter);
            var partyObject = partyCursor.First();
            partyObject.Rsvps.Add(myUserId);

            _partyCollection.FindOneAndReplace(partyFilter, partyObject);
        }

        public async Task<User> GetUserAsync(string username) 
        {

            
            return await _userCollection.Find( x => x.Username == username).FirstAsync();

        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _userCollection.Find(new BsonDocument()).ToListAsync();
            
        }

        public async void FollowUser(string myUserId, string theirUserId)
        {
            var theirFilter = Builders<User>.Filter.Eq(x => x.Id, theirUserId);
            var theirCursor =await _userCollection.FindAsync<User>(theirFilter);
            var theirUser= theirCursor.First();
            var myFilter = Builders<User>.Filter.Eq(x => x.Id, myUserId);
            var myCursor = await _userCollection.FindAsync<User>(myFilter);
            var myUser = myCursor.First();
            if (theirUser != null && myUser != null)
            {
                if (theirUser.Following.Contains(myUser.Id))
                {
                    myUser.Following.Add(theirUserId);
                    myUser.Friends.Add(theirUserId);
                    theirUser.Friends.Add(myUserId);
                    theirUser.Followers.Add(myUserId);
                }
                else
                {
                    myUser.Following.Add(theirUserId);
                    theirUser.Followers.Add(myUserId);
                }

                _userCollection.FindOneAndReplace<User>(myFilter, myUser);
                _userCollection.FindOneAndReplace(theirFilter, theirUser);
                    
            }


            






        }

        //GetNearbyParties returns post because all posts are about parties(duh)
        public async Task<List<Party>> PartiesInsideZipAsync(string postalCode)
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
