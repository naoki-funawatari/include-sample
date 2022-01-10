namespace include_sample.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
