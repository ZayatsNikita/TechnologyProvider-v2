namespace IntegrationTests.Core
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RestEase;
    using TechnologyProvider.Cqrs.Queries.Categories.Core;

    public interface ICategoryRadarApi
    {
        [Get("​Technologies/GetAll")]
        Task<IEnumerable<CategoryResponseModel>> GetAllTechnologies();
    }
}
