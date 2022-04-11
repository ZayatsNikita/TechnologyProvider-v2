using MediatR;
using TechnologyProvider.Cqrs.Core;

namespace TechnologyProvider.Cqrs.Commands.Categories.Delete
{
    public class DeleteCategoryRequest : IRequest<Result<object>>
    {
        public int Id { get; set; }
    }
}
