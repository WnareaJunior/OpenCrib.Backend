namespace OpenCrib.api.Models
{
    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UserCollection { get; set; } = null!;
        public string PartyCollection { get; set; } = null!;
        public string ZipCodeCollection { get; set; } = null!;

    }
}
