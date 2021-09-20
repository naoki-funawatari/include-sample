using include_sample.Models;
using Microsoft.EntityFrameworkCore;

namespace JuniorTennis.Infrastructure.DataBase
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options) { }

        public DbSet<FirstLayer> FirstLayers { get; set; }
        public DbSet<SecondLayer> SecondLayers { get; set; }
        public DbSet<ThirdLayer> ThirdLayers { get; set; }
        public DbSet<FourthLayer> FourthLayers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FirstLayer>()
                .ToTable("first_layers");

            modelBuilder.Entity<SecondLayer>()
                .ToTable("second_layers")
                .HasOne(p => p.FirstLayer)
                .WithMany(b => b.SecondLayers)
                .HasForeignKey(s => s.FirstLayerId)
                .HasPrincipalKey(c => c.Id);

            modelBuilder.Entity<ThirdLayer>()
                .ToTable("third_layers")
                .HasOne(p => p.SecondLayer)
                .WithMany(b => b.ThirdLayers)
                .HasForeignKey(s => s.SecondLayerId)
                .HasPrincipalKey(c => c.Id);

            modelBuilder.Entity<FourthLayer>()
                .ToTable("fourth_layers")
                .HasOne(p => p.ThirdLayer)
                .WithMany(b => b.FourthLayers)
                .HasForeignKey(s => s.ThirdLayerId)
                .HasPrincipalKey(c => c.Id);
        }
    }
}
