using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;

namespace TechnologyProvider.DataAccess.Infrastructure.Extensions
{
    /// <summary>
    /// A class describing extension methods for dependency injection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Method for implementing DbContext as dependencies.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="connectionString">Connection string.</param>
        public static void AddStorage(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TechnologyProviderDbContext>(options => options.UseNpgsql(connectionString));
        }
    }
}
