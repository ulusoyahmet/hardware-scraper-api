using Microsoft.EntityFrameworkCore;
using HardwareScraper.Core.Entities;

namespace HardwareScraper.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Brand).IsRequired();
                entity.Property(e => e.Category).IsRequired();
                entity.Property(e => e.ImageUrl).IsRequired();
                entity.Property(e => e.ProductUrl).IsRequired();
                entity.Property(e => e.Source).IsRequired();
            });
        }
    }
} 