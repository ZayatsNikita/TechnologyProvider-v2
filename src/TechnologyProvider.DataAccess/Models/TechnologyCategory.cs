namespace TechnologyProvider.DataAccess.Models
{
    /// <summary>
    /// A class describing the relationship between technology and category.
    /// </summary>
    public class TechnologyCategory
    {
        /// <summary>
        /// Gets or sets id of the technology.
        /// </summary>
        public int TechnologyId { get; set; }

        /// <summary>
        /// Gets or sets id of the category.
        /// </summary>
        public int CategoryId { get; set; }
    }
}
