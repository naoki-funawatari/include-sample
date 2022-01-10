using System.Collections.Generic;

namespace include_sample.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<Image> Images { get; set; }
        public List<Comment> Comments { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
