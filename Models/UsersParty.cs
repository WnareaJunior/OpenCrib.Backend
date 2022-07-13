using MongoDB.Bson.Serialization.Attributes;

namespace OpenCrib.api.Models
{
    public class UsersParty
    {
        

        

       

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