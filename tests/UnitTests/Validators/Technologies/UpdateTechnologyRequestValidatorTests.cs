using FluentValidation.TestHelper;
using TechnologyProvider.Cqrs.Commands.Technologies.Update;
using Xunit;

namespace UnitTests.Validators.Technologies
{
    public class UpdateTechnologyRequestValidatorTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        public void Validate_WhenIdIsValid_ShouldNotReturnErrors(int id)
        {
            // Arrange
            var validator = new UpdateTechnologyRequestValidator();
            var request = new UpdateTechnologyRequest { Id = id };

            // Act
            var validationResult = validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Validate_WhenIdIsInvalid_ShouldReturnErrors(int id)
        {
            // Arrange
            var validator = new UpdateTechnologyRequestValidator();
            var request = new UpdateTechnologyRequest { Id = id };

            // Act
            var validationResult = validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}
