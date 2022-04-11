using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechnologyProvider.DataAccess.Services;

namespace TechnologyProvider.DataAccess.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddStorage(this IServiceCollection services)
        {
            services.AddDbContext<TechnologyProviderDbContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=release_db;Username=mikita;Password=sicretPassword"));
        }
    }
}
