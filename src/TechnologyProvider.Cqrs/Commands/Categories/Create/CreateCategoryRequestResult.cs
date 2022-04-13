using TechnologyProvider.Cqrs.Commands.Categories.Core;

namespace TechnologyProvider.Cqrs.Commands.Categories.Create
{
    /// <summary>
    /// Represents an object that is sent to the user after the successful creation of the category.
    /// </summary>
    public class CreateCategoryRequestResult : CategoryModel
    {
        /// <summary>
        /// Gets or sets category id.
        /// </summary>
        public int Id { get; set; }
    }
}
