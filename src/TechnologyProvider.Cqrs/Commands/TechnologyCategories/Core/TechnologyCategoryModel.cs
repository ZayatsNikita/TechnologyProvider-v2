namespace TechnologyProvider.Cqrs.Commands.TechnologyCategories.Core
{
    /// <summary>
    /// A model describing the relationship between technology and category.
    /// </summary>
    public class TechnologyCategoryModel
    {
        /// <summary>
        /// Gets or sets category id.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets technology id.
        /// </summary>
        public int TechnologyId { get; set; }
    }
}
