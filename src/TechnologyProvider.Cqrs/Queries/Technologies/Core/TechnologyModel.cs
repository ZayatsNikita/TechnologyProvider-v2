namespace TechnologyProvider.Cqrs.Queries.Technologies.Core
{
    public class TechnologyModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public List<CategoryModel>? Categories { get; set; }
    }
}
