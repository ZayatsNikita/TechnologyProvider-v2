using MediatR;
using TechnologyProvider.Cqrs.Commands.Categories.Core;
using TechnologyProvider.Cqrs.Core;

namespace TechnologyProvider.Cqrs.Commands.Categories.Create
{
    public class CreateCategoryRequest : CategoryModel, IRequest<Result<CreateCategoryResultModel>>
    {
    }
}
