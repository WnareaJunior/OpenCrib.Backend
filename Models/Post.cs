namespace OpenCrib.api.Models
{
    public class Post
    {
        public int views { get; set; }
        public List<string>? rsvps { get; set; }
        public List<Comment>? comments { get; set; }

    }
}
