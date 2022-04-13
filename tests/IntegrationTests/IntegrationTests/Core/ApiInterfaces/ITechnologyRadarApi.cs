namespace IntegrationTests.Core
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RestEase;
    using TechnologyProvider.Cqrs.Queries.Technologies.Core;

    public interface ITechnologyRadarApi
    {
        [Get("​Technologies/GetAll")]
        Task<IEnumerable<TechnologyResponseModel>> GetAllTechnologies();
    }
}
