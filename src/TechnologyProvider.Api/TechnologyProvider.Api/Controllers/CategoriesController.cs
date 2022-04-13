namespace TechnologyProvider.Api.Controllers
{
    using System.Net;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using TechnologyProvider.Api.Infrastructure.Extensions;
    using TechnologyProvider.Cqrs.Commands.Categories.Create;
    using TechnologyProvider.Cqrs.Commands.Categories.Delete;
    using TechnologyProvider.Cqrs.Commands.Categories.Update;
    using TechnologyProvider.Cqrs.Queries.Categories.Core;
    using TechnologyProvider.Cqrs.Queries.Categories.GetAll;
    using TechnologyProvider.Cqrs.Queries.Categories.GetById;

    /// <summary>
    /// Controller performing operations with technology categories.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator object.</param>
        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// A method that returns all existing technology categories.
        /// </summary>
        /// <returns>Collection of <see cref="CategoryResponseModel"/>.</returns>
        [ProducesResponseType(typeof(IEnumerable<CategoryResponseModel>), (int)HttpStatusCode.OK)]
        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllRequest();

            var response = await this.mediator.Send(request);

            return response.IsSuccess
                ? this.Ok(response.Payload)
                : response.ToActionResult();
        }

        /// <summary>
        /// A method that allows you to get a category by its id.
        /// </summary>
        /// <param name="request">Request with id information.</param>
        /// <returns><see cref="CategoryResponseModel"/> object.</returns>
        [ProducesResponseType(typeof(CategoryResponseModel), (int)HttpStatusCode.OK)]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute]GetCategoryByIdRequest request)
        {
            var response = await this.mediator.Send(request);

            return response.IsSuccess
                ? this.Ok(response.Payload)
                : response.ToActionResult();
        }

        /// <summary>
        /// A method for creating a new category.
        /// </summary>
        /// <param name="request">Request containing information about the category.</param>
        /// <returns>Created category object.</returns>
        [ProducesResponseType(typeof(CategoryResponseModel), (int)HttpStatusCode.Created)]
        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            var response = await this.mediator.Send(request);

            return response.IsSuccess
                ? this.CreatedAtAction(nameof(this.GetById), new { Id = response.Payload }, response.Payload)
                : response.ToActionResult();
        }

        /// <summary>
        /// A method for updating an existing category.
        /// </summary>
        /// <param name="request">A request containing the information required for the update.</param>
        /// <returns>Status code with a description of the request result.</returns>
        [HttpPost("Update/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(UpdateCategoryRequest request)
        {
            var response = await this.mediator.Send(request);

            return response.IsSuccess
                ? this.NoContent()
                : response.ToActionResult();
        }

        /// <summary>
        /// A method that allows you to delete an existing category. All links (category-technology) will be deleted automatically.
        /// </summary>
        /// <param name="request">A request containing the information necessary to determine the category to be deleted.</param>
        /// <returns>Status code with a description of the request result.</returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteCategoryRequest request)
        {
            var response = await this.mediator.Send(request);

            return response.IsSuccess
                ? this.NoContent()
                : response.ToActionResult();
        }
    }
}
