using include_sample.Models;
using Microsoft.EntityFrameworkCore;

namespace JuniorTennis.Infrastructure.DataBase
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options) { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .ToTable("blogs");

            modelBuilder.Entity<User>()
                .ToTable("users")
                .HasOne(p => p.Blog)
                .WithMany(b => b.Users)
                .HasForeignKey(s => s.BlogId);

            modelBuilder.Entity<Post>()
                .ToTable("posts")
                .HasOne(p => p.Blog)
                .WithMany(b => b.Posts)
                .HasForeignKey(s => s.BlogId);

            modelBuilder.Entity<Image>()
                .ToTable("images")
                .HasOne(p => p.Post)
                .WithMany(b => b.Images)
                .HasForeignKey(s => s.PostId);

            modelBuilder.Entity<Comment>()
                .ToTable("comments")
                .HasOne(p => p.Post)
                .WithMany(b => b.Comments)
                .HasForeignKey(s => s.PostId);
        }
    }
}
