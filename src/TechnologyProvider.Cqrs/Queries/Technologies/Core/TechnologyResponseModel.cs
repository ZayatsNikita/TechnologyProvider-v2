namespace TechnologyProvider.Cqrs.Queries.Technologies.Core
{
    /// <summary>
    /// A model containing information about the technology to be sent to the user.
    /// </summary>
    public class TechnologyResponseModel
    {
        /// <summary>
        /// Gets or sets name of the technology.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets description of the technology.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets id of the technology.
        /// </summary>
        public int Id { get; set; }
    }
}
