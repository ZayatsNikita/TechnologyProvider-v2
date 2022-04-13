using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Core;
using TechnologyProvider.Cqrs.Commands.Technologies.Core;
using TechnologyProvider.Cqrs.Commands.Technologies.Create;
using TechnologyProvider.Cqrs.Queries.Technologies.Core;
using TechnologyProvider.DataAccess.Models;
using Xunit;

namespace IntegrationTests.Controllers
{
    [Collection(nameof(IntegrationCollection))]
    public class TechnologyControllerTests : TestBase
    {
        public const string MediaType = "application/json";

        public TechnologyControllerTests(TestFixture fixture)
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
            var technology1 = new Technology
            {
                Name = "t_name1",
                Description = "t_description1",
            };
            var technology2 = new Technology
            {
                Name = "t_name2",
                Description = "t_description2",
            };

            this.DbContext.Technologies.Add(technology1);
            this.DbContext.Technologies.Add(technology2);
            this.DbContext.SaveChanges();

            var expected = new List<TechnologyResponseModel>()
            {
                new TechnologyResponseModel
                {
                    Name = technology1.Name,
                    Description = technology1.Description,
                    Id = technology1.Id,
                },
                new TechnologyResponseModel
                {
                    Id = technology2.Id,
                    Name = technology2.Name,
                    Description = technology2.Description,
                }
            };

            // Act
            var response = await this.Client.GetAsync("Technologies/GetAll");
            var contentAsString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TechnologyResponseModel[]>(contentAsString, this.JsonSerializerOptions);

            // Assert
            foreach (var item in expected)
            {
                result.Should().ContainEquivalentOf(item);
            }
        }

        [Fact]
        public async Task GetAllByCategoryId_WhenAllWorkCorectly_ShouldReturnCollectionOfModels()
        {
            // Arrange
            var technology1 = new Technology
            {
                Name = "t_name7",
                Description = "t_description7",
            };
            var technology2 = new Technology
            {
                Name = "t_name8",
                Description = "t_description8",
            };
            var category = new Category
            {
                Name = "category_1"
            };

            this.DbContext.Technologies.Add(technology1);
            this.DbContext.Technologies.Add(technology2);
            this.DbContext.Categories.Add(category);
            this.DbContext.SaveChanges();
            this.DbContext.TechnologyCategories.Add(new TechnologyCategory
            {
                CategoryId = category.Id,
                TechnologyId = technology1.Id,
            });
            this.DbContext.SaveChanges();

            var expected = new TechnologyResponseModel
            {
                Name = technology1.Name,
                Description = technology1.Description,
                Id = technology1.Id,
            };
            var notExpected = new TechnologyResponseModel
            {
                Id = technology2.Id,
                Description = technology2.Description,
                Name = technology2.Name,
            };

            // Act
            var response = await this.Client.GetAsync($"Technologies/GetAllByCategoryId/{category.Id}");

            var contentAsString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<TechnologyResponseModel[]>(contentAsString, this.JsonSerializerOptions);

            // Assert
            result.Should().NotContainEquivalentOf(notExpected);
            result.Should().ContainEquivalentOf(expected);
        }

        [Fact]
        public async Task GetById_WhenEntityExsist_ShouldReturnEntity()
        {
            // Arrange
            var technology = new Technology
            {
                Name = "t_name3",
                Description = "t_description3",
            };

            this.DbContext.Technologies.Add(technology);
            this.DbContext.SaveChanges();

            var expected = new TechnologyResponseModel
            {
                Name = technology.Name,
                Description = technology.Description,
                Id = technology.Id,
            };

            // Act
            var response = await this.Client.GetAsync($"Technologies/GetById/{expected.Id}");

            var contentAsString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<TechnologyResponseModel>(contentAsString, this.JsonSerializerOptions);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task Delete_WhenEntityExsist_ShouldDeleteEntity()
        {
            // Arrange
            var technology = new Technology
            {
                Name = "t_name4",
                Description = "t_description4",
            };
            this.DbContext.Technologies.Add(technology);
            this.DbContext.SaveChanges();

            // Act
            var response = await this.Client.DeleteAsync($"Technologies/Delete/{technology.Id}");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            this.DbContext.Technologies.Should().NotContainEquivalentOf(technology);
        }

        [Fact]
        public async Task Update_WhenEntityExsist_ShouldUpdateEntity()
        {
            // Arrange
            var technology = new Technology
            {
                Name = "t_name5",
                Description = "t_description5",
            };
            var model = new TechnologyModel
            {
                Name = technology.Name + "-NEW",
                Description = technology.Description + "-NEW",
            };
            this.DbContext.Technologies.Add(technology);
            this.DbContext.SaveChanges();
            var contecntBody = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, MediaType);
            technology.Name = model.Name;
            technology.Description = model.Description;

            // Act
            var response = await this.Client.PostAsync($"Technologies/Update/{technology.Id}", contecntBody);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            this.DbContext.Technologies.Should().ContainEquivalentOf(technology);
        }

        [Fact]
        public async Task Create_WhenEntityExsist_ShouldCreateEntity()
        {
            // Arrange
            var expected = new Technology
            {
                Name = "t_name10",
                Description = "t_description10",
            };
            var model = new CreateTechnologyRequest
            {
                Name = expected.Name,
                Description = expected.Description,
            };

            var contecntBody = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, MediaType);
            expected.Name = model.Name;
            expected.Description = model.Description;

            // Act
            var response = await this.Client.PostAsync($"Technologies/Create", contecntBody);
            var contentAsString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<CreateTechnologyRequestResult>(contentAsString, this.JsonSerializerOptions);
            expected.Id = result.Id;

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            this.DbContext.Technologies.Should().ContainEquivalentOf(expected);
        }
    }
}
