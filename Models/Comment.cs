namespace OpenCrib.api.Models
{
   
    public class Comment
    {
        public Comment(string id, string commt)
        {
            _id = id;
            comment = commt;
        }
        public string _id { get; set; }
        public string comment { get; set; }   
    }
}
