namespace IntegrationTests.Core
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using RestEase;

    public class TestFixture : TestFixtureBase
    {
        public ITechnologyRadarApi TechnologyRadarApi
        {
            get
            {
                return RestClient.For<ITechnologyRadarApi>(this.HttpClient);
            }
        }

        public ICategoryRadarApi CategoryRadarApi
        {
            get
            {
                return RestClient.For<ICategoryRadarApi>(this.HttpClient);
            }
        }

        protected override string TestEnvironmentName => "zayatsMikita_testContainer";

        private HttpClient HttpClient => this.Factory.CreateClient();

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
        }
    }
}
