using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TechnologyProvider.DataAccess.Infrastructure.Extensions;

namespace TechnologyProvider.Cqrs.Infrastructure.Extensions
{
    /// <summary>
    /// Extensions for the serivis collection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// The method of the embedding command and queries in you container.
        /// </summary>
        /// <param name="services">List of services.</param>
        /// <param name="connectionString">Storage Connection string.</param>
        public static void AddCommandsAndQueriesHandlers(this IServiceCollection services, string connectionString)
        {
            services.AddStorage(connectionString);
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
