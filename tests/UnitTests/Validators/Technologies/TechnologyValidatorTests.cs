using FluentValidation.TestHelper;
using System.Collections.Generic;
using TechnologyProvider.Cqrs.Commands.Technologies.Core;
using Xunit;

namespace UnitTests.Validators.Technologies
{
    public class TechnologyValidatorTests
    {
        [Theory, MemberData(nameof(TestDataForInvalidNames))]
        public void Validate_WhenNameIsIncorect_ShouldReturnError(string name)
        {
            // Arrange
            var validator = new TechonologyModelValidator();
            var request = new TechnologyModel
            {
                Name = name,
            };

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(model => model.Name);
        }

        [Theory, MemberData(nameof(TestDataForValidNames))]
        public void Validate_WhenNameIsCorect_ShouldntReturnError(string? name)
        {
            // Arrange
            var validator = new TechonologyModelValidator();
            var request = new TechnologyModel
            {
                Name = name,
            };

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Theory, MemberData(nameof(TestDataForInvalidDescription))]
        public void Validate_WhenDescriptionIsIncorect_ShouldReturnError(string? description)
        {
            // Arrange
            var validator = new TechonologyModelValidator();
            var request = new TechnologyModel
            {
                Description = description,
            };

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Theory, MemberData(nameof(TestDataForValidDescription))]
        public void Validate_WhenDescriptionIsCorect_ShouldntReturnError(string? description)
        {
            // Arrange
            var validator = new TechonologyModelValidator();
            var request = new TechnologyModel
            {
                Description = description,
            };

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }

        [Theory, MemberData(nameof(TestDataForInvalidIds))]
        public void Validate_WhenCategoryIdsIsIncorect_ShouldReturnError(IEnumerable<int>? ids)
        {
            // Arrange
            var validator = new TechonologyModelValidator();
            var request = new TechnologyModel
            {
                CategoryIds = ids,
            };

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CategoryIds);
        }

        [Fact]
        public void Validate_WhenCategoryIdsIsCorect_ShouldNotReturnError()
        {
            // Arrange
            var validator = new TechonologyModelValidator();
            var request = new TechnologyModel
            {
                CategoryIds = new List<int> { 1 },
            };

            // Act
            var result = validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.CategoryIds);
        }

        public static IEnumerable<object[]> TestDataForInvalidNames()
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

        public static IEnumerable<object[]> TestDataForValidNames()
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

        public static IEnumerable<object[]> TestDataForValidDescription()
        {
            var testData = new List<string[]>
            {
                new string[]
                {
                    string.Empty,
                },
                new string[]
                {
                    null,
                },
                new string[]
                {
                    new string('a', ValidationConstants.MaxDescriptionLength),
                },
            };

            return testData;
        }

        public static IEnumerable<object[]> TestDataForInvalidDescription()
        {
            var testData = new List<string[]>
            {
                new string[]
                {
                    new string('a', ValidationConstants.MaxDescriptionLength + 1),
                },
            };

            return testData;
        }

        public static IEnumerable<object[]> TestDataForInvalidIds()
        {
            var testData = new List<object[]>
            {
                new List<int>[]
                {
                    new List<int>(),
                },
                new List<int>[]
                {
                    null,
                },
            };

            return testData;
        }

    }
}
