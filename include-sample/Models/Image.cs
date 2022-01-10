namespace include_sample.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Url { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
