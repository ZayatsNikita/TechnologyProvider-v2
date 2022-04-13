using MediatR;
using TechnologyProvider.Cqrs.Commands.TechnologyCategories.Core;
using TechnologyProvider.Cqrs.Core;

namespace TechnologyProvider.Cqrs.Commands.TechnologyCategories.Create
{
    /// <summary>
    /// Request to create technology-category model.
    /// </summary>
    public class CreateTechnologyCategoryRequest : TechnologyCategoryModel, IRequest<Result<int>>
    {
    }
}
