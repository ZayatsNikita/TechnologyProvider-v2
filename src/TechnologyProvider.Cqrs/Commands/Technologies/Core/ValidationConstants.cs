namespace TechnologyProvider.Cqrs.Commands.Technologies.Core
{
    /// <summary>
    /// Constants to validate technologies.
    /// </summary>
    public static class ValidationConstants
    {
        /// <summary>
        /// Minimal length for technologty name.
        /// </summary>
        public const int MinNameLength = 1;

        /// <summary>
        /// Maximal length for technologty name.
        /// </summary>
        public const int MaxNameLength = 50;

        /// <summary>
        /// Minimal length for technologty description.
        /// </summary>
        public const int MaxDescriptionLength = 100;
    }
}
