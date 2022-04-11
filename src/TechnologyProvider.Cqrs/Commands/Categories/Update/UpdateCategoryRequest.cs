using MediatR;
using TechnologyProvider.Cqrs.Commands.Categories.Core;
using TechnologyProvider.Cqrs.Core;

namespace TechnologyProvider.Cqrs.Commands.Categories.Update
{
    public class UpdateCategoryRequest : CategoryModel, IRequest<Result<UpdateCategoryResultModel>>
    {
        public int Id { get; set; }
    }
}
