namespace TechnologyProvider.Cqrs.Queries.Categories.Core
{
    /// <summary>
    /// Category model to send to the end user.
    /// </summary>
    public class CategoryResponseModel
    {
        /// <summary>
        /// Gets or sets category id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets category name.
        /// </summary>
        public string Name { get; set; }
    }
}
