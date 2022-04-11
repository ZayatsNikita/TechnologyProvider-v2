using MediatR;
using TechnologyProvider.Cqrs.Core;
using TechnologyProvider.Cqrs.Queries.Categories.Core;

namespace TechnologyProvider.Cqrs.Queries.Categories.GetById
{
    public class GetByIdRequest : IRequest<Result<CategoryModel>>
    {
        public int Id { get; set; }
    }
}
