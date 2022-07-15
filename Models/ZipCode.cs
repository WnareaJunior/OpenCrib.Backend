using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OpenCrib.api.Models
{
    public class ZipCode
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("zipcode")]
        public string Zip { get; set; }

        public string get() { return Zip; }
        public void set(string zip) { Zip = zip; }
    }
}
