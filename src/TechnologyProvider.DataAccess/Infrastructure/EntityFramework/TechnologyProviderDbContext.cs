using Microsoft.EntityFrameworkCore;
using TechnologyProvider.DataAccess.Models;

namespace TechnologyProvider.DataAccess.Infrastructure.EntityFramework
{
    /// <summary>
    /// Database context.
    /// </summary>
    public class TechnologyProviderDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TechnologyProviderDbContext"/> class.
        /// </summary>
        /// <param name="options">DbContextOptions for <see cref="TechnologyProviderDbContext"/>.</param>
        public TechnologyProviderDbContext(DbContextOptions<TechnologyProviderDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets technologies dbSet.
        /// </summary>
        public DbSet<Technology> Technologies { get; set; }

        /// <summary>
        /// Gets or sets categories dbSet.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets TechnologyCategory pairs dbSet.
        /// </summary>
        public DbSet<TechnologyCategory> TechnologyCategories { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));

            modelBuilder.Entity<TechnologyCategory>(entity =>
            {
                entity.HasKey(e => new { e.TechnologyId, e.CategoryId });
                entity.HasOne<Technology>().WithMany().HasForeignKey(e => e.TechnologyId);
                entity.HasOne<Category>().WithMany().HasForeignKey(e => e.CategoryId);
            });
        }
    }
}
