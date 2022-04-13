namespace TechnologyProvider.Cqrs.Commands.Technologies.Core;

/// <summary>
/// A model describing the technology.
/// </summary>
public class TechnologyModel
{
    /// <summary>
    /// Gets or sets technology Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets description of the technology.
    /// </summary>
    public string Description { get; set; }
}
