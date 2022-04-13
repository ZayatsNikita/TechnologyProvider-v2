namespace IntegrationTests.Core
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;
    using TestEnvironment.Docker;
    using TestEnvironment.Docker.Containers.Postgres;
    using Xunit;

    public abstract class TestFixtureBase : IAsyncLifetime
    {
        private const string PostgresContainerKey = "psql";
        private IDockerEnvironment dockerEnvironment;

        public string PostgresConnectionString { get; private set; }

        internal WebApplicationFactory<Program>? Factory { get; set; }

        protected abstract string TestEnvironmentName { get; }

        public virtual async Task InitializeAsync()
        {
            this.dockerEnvironment = await this.PrepareDockerEnvironment();
            this.PostgresConnectionString = this.dockerEnvironment.GetContainer<PostgresContainer>(PostgresContainerKey).GetConnectionString();

            this.ConfigureTestServer(this.PostgresConnectionString);

            using var scope = this.Factory.Services.CreateScope();
            var dbContect = scope.ServiceProvider.GetService<TechnologyProviderDbContext>();
            await dbContect.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            this.Factory.Dispose();
            await this.dockerEnvironment.DisposeAsync();
        }

        protected async Task<IDockerEnvironment> PrepareDockerEnvironment()
        {
            var environmentBuilder = new DockerEnvironmentBuilder();

            var dockerEnvironmentBuilder = environmentBuilder
                .SetName(this.TestEnvironmentName)
                .AddPostgresContainer(PostgresContainerKey);

            dockerEnvironmentBuilder.UseWsl2();

            var env = dockerEnvironmentBuilder.Build();

            await env.UpAsync();

            return env;
        }

        protected void ConfigureTestServer(string postgresConnectionString)
        {
            this.Factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptior = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<TechnologyProviderDbContext>));
                        if (descriptior is not null)
                        {
                            services.Remove(descriptior);
                        }

                        services.AddDbContext<TechnologyProviderDbContext>(options => options.UseNpgsql(postgresConnectionString));
                    });
                });

            var server = this.Factory.Server;
        }
    }
}
