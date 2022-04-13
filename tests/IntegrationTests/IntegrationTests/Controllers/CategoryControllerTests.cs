using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Core;
using TechnologyProvider.Cqrs.Commands.Categories.Create;
using TechnologyProvider.Cqrs.Queries.Categories.Core;
using TechnologyProvider.DataAccess.Models;
using Xunit;

namespace IntegrationTests.Controllers
{
    [Collection(nameof(IntegrationCollection))]
    public class CategoryControllerTests : TestBase
    {
        public const string MediaType = "application/json";

        public CategoryControllerTests(TestFixture fixture)
            : base(fixture)
        {
            this.JsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public JsonSerializerOptions JsonSerializerOptions { get; set; }

        [Fact]
        public async Task GetAll_WhenAllWorkCorectly_ShouldReturnCollectionOfModels()
        {
            // Arrange
            var category = new Category
            {
                Name = "c_name5",
            };
            var category1 = new Category
            {
                Name = "c_name6",
            };

            this.DbContext.Categories.Add(category);
            this.DbContext.Categories.Add(category1);
            this.DbContext.SaveChanges();

            var expected = new List<CategoryResponseModel>()
            {
                new CategoryResponseModel
                {
                    Name = category.Name,
                    Id = category.Id,
                },
                new CategoryResponseModel
                {
                    Id = category1.Id,
                    Name = category1.Name,
                }
            };

            // Act
            var response = await this.Client.GetAsync("Categories/GetAll");

            var contentAsString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CategoryResponseModel[]>(contentAsString, this.JsonSerializerOptions);

            // Assert
            foreach (var item in expected)
            {
                result.Should().ContainEquivalentOf(item);
            }
        }

        [Fact]
        public async Task GetById_WhenEntityExsist_ShouldReturnEntity()
        {
            // Arrange
            var category = new Category
            {
                Name = "c_name20",
            };

            this.DbContext.Categories.Add(category);
            this.DbContext.SaveChanges();

            var expected = new CategoryResponseModel
            {
                Name = category.Name,
                Id = category.Id,
            };

            // Act
            var response = await this.Client.GetAsync($"Categories/GetById/{expected.Id}");
            var contentAsString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<CategoryResponseModel>(contentAsString, this.JsonSerializerOptions);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task Delete_WhenEntityExsist_ShouldDeleteEntity()
        {
            // Arrange
            var category = new Category
            {
                Name = "c_name15",
            };
            this.DbContext.Categories.Add(category);
            this.DbContext.SaveChanges();

            // Act
            var response = await this.Client.DeleteAsync($"Categories/Delete/{category.Id}");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            this.DbContext.Categories.Should().NotContainEquivalentOf(category);
        }

        [Fact]
        public async Task Create_WhenEntityExsist_ShouldCreateEntity()
        {
            // Arrange
            var expected = new Category
            {
                Name = "c_name100",
            };
            var model = new CreateCategoryRequest
            {
                Name = expected.Name,
            };

            var contecntBody = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, MediaType);
            expected.Name = model.Name;

            // Act
            var response = await this.Client.PostAsync($"Categories/Create", contecntBody);
            var contentAsString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<CreateCategoryRequestResult>(contentAsString, this.JsonSerializerOptions);
            expected.Id = result.Id;

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            this.DbContext.Categories.Should().ContainEquivalentOf(expected);
        }
    }
}
