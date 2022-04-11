namespace TechnologyProvider.Cqrs.Commands.Technologies.Core;

public class TechnologyModel
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public IEnumerable<int>? CategoryIds { get; set; }
}
