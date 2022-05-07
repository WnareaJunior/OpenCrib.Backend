namespace OpenCrib.api.Models
{
    /// <summary>
    /// Address
    /// </summary>
    public class Address
    {
        public string addressLine1 { get; set; }
        public string? addressLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string  postalCode { get; set; }
    }
}
