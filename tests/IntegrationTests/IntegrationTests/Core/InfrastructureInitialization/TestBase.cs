namespace IntegrationTests.Core
{
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using TechnologyProvider.DataAccess.Infrastructure.EntityFramework;

    public class TestBase
    {
        private readonly IServiceScope serviceScope;

        public TestBase(TestFixture fixture)
        {
            this.Factory = fixture.Factory;
            this.TechnologyRadarApi = fixture.TechnologyRadarApi;
            this.serviceScope = fixture.Factory.Services.CreateScope();
            this.DbContext = this.serviceScope.ServiceProvider.GetService<TechnologyProviderDbContext>();
        }

        public ITechnologyRadarApi TechnologyRadarApi { get; private set; }

        public ICategoryRadarApi CategoryRadarApi { get; private set; }

        public TechnologyProviderDbContext DbContext { get; private set; }

        public HttpClient Client => this.Factory.CreateClient();

        internal WebApplicationFactory<Program> Factory { get; private set; }
    }
}
