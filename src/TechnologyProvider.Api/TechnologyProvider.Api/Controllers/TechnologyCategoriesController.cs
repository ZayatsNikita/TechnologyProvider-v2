namespace TechnologyProvider.Api.Controllers
{
    using System.Net;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using TechnologyProvider.Api.Infrastructure.Extensions;
    using TechnologyProvider.Cqrs.Commands.TechnologyCategories.Create;
    using TechnologyProvider.Cqrs.Commands.TechnologyCategories.Delete;
    using TechnologyProvider.Cqrs.Queries.TechnologyCategories.Core;
    using TechnologyProvider.Cqrs.Queries.TechnologyCategories.GetAllByParams;

    /// <summary>
    /// Controller for controlling pairs Technology Category.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public class TechnologyCategoriesController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechnologyCategoriesController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator object.</param>
        public TechnologyCategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// A method that returns all existing technology-category pairs.
        /// </summary>
        /// <param name="request">Request object.</param>
        /// <returns>Collection of <see cref="CategoryResponseModel"/>.</returns>
        [ProducesResponseType(typeof(IEnumerable<TechnologyCategoriesResponseModel>), (int)HttpStatusCode.OK)]
        [HttpGet("GetAll/CategoryId/{CategoryId=0}/TechnologyId/{TechnologyId=0}")]
        public async Task<IActionResult> GetAll(GetAllByParamsRequest request)
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
        [ProducesResponseType(typeof(TechnologyCategoriesResponseModel), (int)HttpStatusCode.Created)]
        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create(CreateTechnologyCategoryRequest request)
        {
            var response = await this.mediator.Send(request);

            return response.IsSuccess
                ? this.CreatedAtAction(nameof(this.GetAll), new { CategoryId = request.CategoryId, TechnologyId = request.TechnologyId }, request)
                : response.ToActionResult();
        }

        /// <summary>
        /// A method that allows you to delete an existing category. All links (category-technology) will be deleted automatically.
        /// </summary>
        /// <param name="request">A request containing the information necessary to determine the category to be deleted.</param>
        /// <returns>Status code with a description of the request result.</returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [HttpDelete("Delete/{CategoryId=0}/{TechnologyId=0}")]
        public async Task<IActionResult> Delete(DeleteTechnologyCategoryRequest request)
        {
            var response = await this.mediator.Send(request);

            return response.IsSuccess
                ? this.NoContent()
                : response.ToActionResult();
        }
    }
}
