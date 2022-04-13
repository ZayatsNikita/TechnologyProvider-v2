namespace UnitTests.Validators.Categories
{
    using System.Collections.Generic;
    using FluentValidation.TestHelper;
    using TechnologyProvider.Cqrs.Commands.Categories.Core;
    using Xunit;

    public class CategoryModelValidationTests
    {
        [Theory, MemberData(nameof(TestDataWithInvalidNames))]
        public void Validate_WhenNameIsIncorect_ShouldReturnError(string name)
        {
            // Arrange
            var validator = new CategoryModelValidator();
            var request = new CategoryModel
            {
                Name = name,
            };

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Theory, MemberData(nameof(TestDataWithValidNames))]
        public void Validate_WhenNameIsCorect_ShouldNotReturnError(string? name)
        {
            // Arrange
            var validator = new CategoryModelValidator();
            var request = new CategoryModel
            {
                Name = name,
            };

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        public static IEnumerable<object[]> TestDataWithInvalidNames()
        {
            var testData = new List<object[]>
            {
                new object[]
                {
                    string.Empty,
                },
                new object[]
                {
                    new string('a', ValidationConstants.MaxNameLength + 1),
                },
                new string[]
                {
                    null,
                }
            };

            return testData;
        }

        public static IEnumerable<object[]> TestDataWithValidNames()
        {
            var testData = new List<object[]>
            {
                new object[]
                {
                    new string('a', ValidationConstants.MinNameLength),
                },
                new object[]
                {
                    new string('a', ValidationConstants.MaxNameLength),
                },
            };

            return testData;
        }
    }
}
