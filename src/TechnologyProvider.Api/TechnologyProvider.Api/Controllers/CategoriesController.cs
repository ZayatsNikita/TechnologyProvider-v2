using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnologyProvider.Api.Infrastructure.Extensions;
using TechnologyProvider.Cqrs.Commands.Categories.Create;
using TechnologyProvider.Cqrs.Commands.Categories.Delete;
using TechnologyProvider.Cqrs.Commands.Categories.Update;
using TechnologyProvider.Cqrs.Queries.Categories.Core;
using TechnologyProvider.Cqrs.Queries.Categories.GetAll;
using TechnologyProvider.Cqrs.Queries.Categories.GetById;


namespace TechnologyProvider.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(typeof(IEnumerable<CategoryModel>), (int)HttpStatusCode.OK)]
        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllRequest();

            var response = await _mediator.Send(request);

            return response.IsSuccess
                ? Ok(response.Payload)
                : response.ToActionResult();
        }

        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromQuery]int id)
        {
            var request = new GetByIdRequest
            {
                Id = id
            };

            var response = await _mediator.Send(request);

            return response.IsSuccess
                ? Ok(response.Payload)
                : response.ToActionResult();
        }

        [ProducesResponseType(typeof(CreateCategoryResultModel), (int)HttpStatusCode.Created)]
        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            var response = await _mediator.Send(request);

            return response.IsSuccess
                ? Ok(response.Payload)
                : response.ToActionResult();
        }

        [ProducesResponseType(typeof(CreateCategoryResultModel), (int)HttpStatusCode.OK)]
        [HttpPost(nameof(Update))]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest request)
        {
            var response = await _mediator.Send(request);

            return response.IsSuccess
                ? Ok(response.Payload)
                : response.ToActionResult();
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpDelete(nameof(Delete))]
        public async Task<IActionResult> Delete([FromBody] DeleteCategoryRequest request)
        {
            var response = await _mediator.Send(request);

            return response.IsSuccess
                ? Ok(response.Payload)
                : response.ToActionResult();
        }
    }
}
