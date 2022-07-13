using MongoDB.Bson.Serialization.Attributes;

namespace OpenCrib.api.Models
{
    /// <summary>
    /// Address
    /// </summary>
    public class Address
    {
        [BsonElement("addressLine1")]
        public string AddressLine1 { get; set; }

        [BsonElement("addressLine2")]
        public string? AddressLine2 { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("state")]
        public string State { get; set; }

        [BsonElement("postalCode")]
        public string  PostalCode { get; set; }
    }
}
