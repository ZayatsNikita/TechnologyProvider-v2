namespace TechnologyProvider.Cqrs.Commands.Categories.Core
{
    /// <summary>
    /// Constants for validating the category model.
    /// </summary>
    public static class ValidationConstants
    {
        /// <summary>
        /// Minimum length of the category name.
        /// </summary>
        public const int MinNameLength = 1;

        /// <summary>
        /// Maximal length of the category name.
        /// </summary>
        public const int MaxNameLength = 50;
    }
}
