using MediatR;
using TechnologyProvider.Cqrs.Commands.Categories.Core;
using TechnologyProvider.Cqrs.Core;

namespace TechnologyProvider.Cqrs.Commands.Categories.Create
{
    /// <summary>
    /// Request to create a category.
    /// </summary>
    public class CreateCategoryRequest : CategoryModel, IRequest<Result<CreateCategoryRequestResult>>
    {
    }
}
