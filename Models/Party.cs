using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OpenCrib.api.Models
{
    public class Party


    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("hostId")]
        public string HostId { get; set; }

        [BsonElement("hostUsername")]
        public string HostUsername { get; set; }

        [BsonElement("partyName")]
        public string PartyName { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }  

        [BsonElement("views")]
        public int Views { get; set; }

        [BsonElement("rsvps")]
        public List<string>? Rsvps { get; set; }

        [BsonElement("comments")]
        public List<Comment>? Comments { get; set; }
        
        [BsonElement("address")]
        public Address? Address { get; set; }




        

    }
}
