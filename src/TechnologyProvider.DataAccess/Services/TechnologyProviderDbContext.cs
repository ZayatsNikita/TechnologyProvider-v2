using Microsoft.EntityFrameworkCore;
using TechnologyProvider.DataAccess.Models;

namespace TechnologyProvider.DataAccess.Services
{
    public class TechnologyProviderDbContext : DbContext
    {
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TechnologyCategory> TechnologyCategories { get; set; }

        public TechnologyProviderDbContext(DbContextOptions<TechnologyProviderDbContext> options)
            : base(options)
        {
        }

        public TechnologyProviderDbContext()
        {
        }

        public void Close()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TechnologyCategory>(entity =>
            {
                entity.HasKey(e => new { e.TechnologyId, e.CategoryId });
                entity.HasOne<Technology>().WithMany().HasForeignKey(e => e.TechnologyId);
                entity.HasOne<Category>().WithMany().HasForeignKey(e => e.CategoryId);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=release_db;Username=mikita;Password=sicretPassword");
        }
    }
}
