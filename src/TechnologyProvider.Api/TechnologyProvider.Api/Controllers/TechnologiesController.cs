using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechnologyProvider.Api.Infrastructure.Extensions;
using TechnologyProvider.Cqrs.Commands.Technologies.Create;
using TechnologyProvider.Cqrs.Commands.Technologies.Delete;
using TechnologyProvider.Cqrs.Commands.Technologies.Update;
using TechnologyProvider.Cqrs.Queries.Technologies.Core;
using TechnologyProvider.Cqrs.Queries.Technologies.GetAll;
using TechnologyProvider.Cqrs.Queries.Technologies.GetByCategoryId;
using TechnologyProvider.Cqrs.Queries.Technologies.GetById;

namespace TechnologyProvider.Api.Controllers
{
    /// <summary>
    /// A controller that allows you to perform operations with technologies.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public class TechnologiesController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechnologiesController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator object.</param>
        public TechnologiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// A method that returns all created technologies.
        /// </summary>
        /// <returns>Collection of <see cref="TechnologyResponseModel"/>.</returns>
        [ProducesResponseType(typeof(IEnumerable<TechnologyResponseModel>), (int)HttpStatusCode.OK)]
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
        /// A method that allows you to get a technology by its id.
        /// </summary>
        /// <param name="request">Request with technology id.</param>
        /// <returns><see cref="TechnologyResponseModel"/> object.</returns>
        [ProducesResponseType(typeof(TechnologyResponseModel), (int)HttpStatusCode.OK)]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] GetTechnologyByIdRequest request)
        {
            var response = await this.mediator.Send(request);

            return response.IsSuccess
                ? this.Ok(response.Payload)
                : response.ToActionResult();
        }

        /// <summary>
        /// A method that allows you to get all the technologies that belong to this category.
        /// </summary>
        /// <param name="request">Request with inofrmation about category.</param>
        /// <returns>Collection of <see cref="TechnologyResponseModel"/>.</returns>
        [ProducesResponseType(typeof(IEnumerable<TechnologyResponseModel>), (int)HttpStatusCode.OK)]
        [HttpGet("GetAllByCategoryId/{id}")]
        public async Task<IActionResult> GetAllByCategoryId([FromRoute] GetByCategoryIdRequest request)
        {
            var response = await this.mediator.Send(request);

            return response.IsSuccess
                ? this.Ok(response.Payload)
                : response.ToActionResult();
        }

        /// <summary>
        /// A method for creating a new technology.
        /// </summary>
        /// <param name="request">Request containing information about the technology.</param>
        /// <returns>Created technology object.</returns>
        [ProducesResponseType(typeof(IEnumerable<TechnologyResponseModel>), (int)HttpStatusCode.Created)]
        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create([FromBody] CreateTechnologyRequest request)
        {
            var response = await this.mediator.Send(request);

            return response.IsSuccess
                ? this.CreatedAtAction(nameof(this.GetById), new { Id = response.Payload }, response.Payload)
                : response.ToActionResult();
        }

        /// <summary>
        /// A method for updating an existing technology.
        /// </summary>
        /// <param name="request">A request containing the information required for the update.</param>
        /// <returns>Status code with a description of the request result.</returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [HttpPost("Update/{Id}")]
        public async Task<IActionResult> Update([FromRoute] UpdateTechnologyRequest request)
        {
            var response = await this.mediator.Send(request);

            return response.IsSuccess
                ? this.NoContent()
                : response.ToActionResult();
        }

        /// <summary>
        /// A method that allows you to delete an existing technology. All links (category-technology) will be deleted automatically.
        /// </summary>
        /// <param name="request">Request with id.</param>
        /// <returns>Status code with a description of the request result.</returns>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete([FromRoute] DeleteTechnologyRequest request)
        {
            var response = await this.mediator.Send(request);

            return response.IsSuccess
                ? this.NoContent()
                : response.ToActionResult();
        }
    }
}
