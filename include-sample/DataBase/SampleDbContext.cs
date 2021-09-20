using include_sample.Models;
using Microsoft.EntityFrameworkCore;

namespace JuniorTennis.Infrastructure.DataBase
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options) { }

        public DbSet<FirstLayer> FirstLayers { get; set; }
        public DbSet<SecondLayerA> SecondLayersA { get; set; }
        public DbSet<SecondLayerB> SecondLayersB { get; set; }
        public DbSet<ThirdLayerA> ThirdLayersA { get; set; }
        public DbSet<ThirdLayerB> ThirdLayersB { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FirstLayer>()
                .ToTable("first_layers");

            modelBuilder.Entity<SecondLayerA>()
                .ToTable("second_layers_a")
                .HasOne(p => p.FirstLayer)
                .WithMany(b => b.SecondLayersA)
                .HasForeignKey(s => s.FirstLayerId)
                .HasPrincipalKey(c => c.Id);

            modelBuilder.Entity<ThirdLayerA>()
                .ToTable("third_layers_a")
                .HasOne(p => p.SecondLayerA)
                .WithMany(b => b.ThirdLayers)
                .HasForeignKey(s => s.SecondLayerAId)
                .HasPrincipalKey(c => c.Id);

            modelBuilder.Entity<SecondLayerB>()
                .ToTable("second_layers_b")
                .HasOne(p => p.FirstLayer)
                .WithMany(b => b.SecondLayersB)
                .HasForeignKey(s => s.FirstLayerId)
                .HasPrincipalKey(c => c.Id);

            modelBuilder.Entity<ThirdLayerB>()
                .ToTable("third_layers_b")
                .HasOne(p => p.SecondLayerB)
                .WithMany(b => b.ThirdLayers)
                .HasForeignKey(s => s.SecondLayerBId)
                .HasPrincipalKey(c => c.Id);
        }
    }
}
