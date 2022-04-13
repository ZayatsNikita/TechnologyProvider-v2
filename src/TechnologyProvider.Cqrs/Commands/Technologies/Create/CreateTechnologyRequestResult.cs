using TechnologyProvider.Cqrs.Commands.Technologies.Core;

namespace TechnologyProvider.Cqrs.Commands.Technologies.Create
{
    /// <summary>
    /// Represents an object that is sent to the user after the successful creation of the technology.
    /// </summary>
    public class CreateTechnologyRequestResult : TechnologyModel
    {
        /// <summary>
        /// Gets or sets technology id.
        /// </summary>
        public int Id { get; set; }
    }
}
