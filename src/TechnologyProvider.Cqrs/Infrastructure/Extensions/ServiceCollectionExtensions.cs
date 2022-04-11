using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TechnologyProvider.DataAccess.Infrastructure.Extensions;

namespace TechnologyProvider.Cqrs.Infrastructure.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommandsAndQueriesHandlers(this IServiceCollection services)
        {
            services.AddStorage();
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
