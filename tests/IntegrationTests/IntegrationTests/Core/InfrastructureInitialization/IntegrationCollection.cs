namespace IntegrationTests.Core
{
    using Xunit;

    /// <summary>
    /// Collection of integration tests.
    /// </summary>
    [CollectionDefinition(nameof(IntegrationCollection))]
    public class IntegrationCollection : ICollectionFixture<TestFixture>
    {
    }
}