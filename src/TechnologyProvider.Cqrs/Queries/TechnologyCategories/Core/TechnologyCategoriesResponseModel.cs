namespace TechnologyProvider.Cqrs.Queries.TechnologyCategories.Core
{
    /// <summary>
    /// Model of technology category pair to send to user.
    /// </summary>
    public class TechnologyCategoriesResponseModel
    {
        /// <summary>
        /// Gets or sets category id.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets technologyId.
        /// </summary>
        public int TechnologyId { get; set; }
    }
}
