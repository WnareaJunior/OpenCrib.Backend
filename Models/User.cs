using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace OpenCrib.api.Models
{
    public class User
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("hostRating")]
        public int HostRating { get; set; } = 0;

        [BsonElement("guestRating")]
        public int GuestRating { get; set; } = 0;

        [BsonElement("followers")]
        public List<string>? Followers { get; set; }

        [BsonElement("following")]
        public List<string>? Following { get; set; }

        [BsonElement("partiesAttended")]
        public int PartiesAttended { get; set; } = 0;

        [BsonElement("partiesThrown")]
        public int PartiesThrown { get; set; } = 0;

        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("posts")]
        public List<Post>? Posts { get; set; }

        [BsonElement("latitude")]
        public double? Latitude { get; set; }

        [BsonElement("longitude")]
        public double? Longitude { get; set; }

        [BsonElement("dob")]
        public DateTime Dob { get; set; }

        [BsonElement("address")]
        public Address? Address { get; set; }

    }
}

