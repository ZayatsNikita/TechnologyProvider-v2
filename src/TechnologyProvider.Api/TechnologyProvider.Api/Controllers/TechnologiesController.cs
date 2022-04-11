using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnologyProvider.Api.Infrastructure.Extensions;
using TechnologyProvider.Cqrs.Commands.Technologies.Create;
using TechnologyProvider.Cqrs.Commands.Technologies.Delete;
using TechnologyProvider.Cqrs.Commands.Technologies.Update;
using TechnologyProvider.Cqrs.Queries.Technologies.GetAll;
using TechnologyProvider.Cqrs.Queries.Technologies.GetByCategoryId;
using TechnologyProvider.Cqrs.Queries.Technologies.GetById;
using QueryTechnologyModel = TechnologyProvider.Cqrs.Queries.Technologies.Core.TechnologyModel;

namespace TechnologyProvider.Api.Controllers
{
    [ApiController]
    [Route("tech/[controller]")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public class TechnologiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TechnologiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(typeof(IEnumerable<QueryTechnologyModel>), (int)HttpStatusCode.OK)]
        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllRequest();

            var response = await _mediator.Send(request);

            return response.IsSuccess
                ? Ok(response.Payload)
                : response.ToActionResult();
        }

        [ProducesResponseType(typeof(QueryTechnologyModel), (int)HttpStatusCode.OK)]
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var request = new GetByIdRequest
            {
                Id = id,
            };

            var response = await _mediator.Send(request);

            return response.IsSuccess
                ? Ok(response.Payload)
                : response.ToActionResult();
        }

        [ProducesResponseType(typeof(IEnumerable<QueryTechnologyModel>), (int)HttpStatusCode.OK)]
        [HttpGet("GetAllByCategoryId/{id}")]
        public async Task<IActionResult> GetAllByCategoryId([FromQuery] int id)
        {
            var request = new GetByCategoryIdRequest
            {
                Id = id,
            };

            var response = await _mediator.Send(request);

            return response.IsSuccess
                ? Ok(response.Payload)
                : response.ToActionResult();
        }

        [ProducesResponseType(typeof(IEnumerable<CreateTechnologyResultModel>), (int)HttpStatusCode.Created)]
        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create([FromBody] CreateTechnologyRequest request)
        {
            var response = await _mediator.Send(request);

            return response.IsSuccess
                ? Ok(response.Payload)
                : response.ToActionResult();
        }

        [ProducesResponseType(typeof(IEnumerable<UpdateTechonologyResultModel>), (int)HttpStatusCode.OK)]
        [HttpPost(nameof(Update))]
        public async Task<IActionResult> Update([FromBody] UpdateTechnologyRequest request)
        {
            var response = await _mediator.Send(request);

            return response.IsSuccess
                ? Ok(response.Payload)
                : response.ToActionResult();
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPost(nameof(Delete))]
        public async Task<IActionResult> Delete([FromBody] DeleteTechnologyRequest request)
        {
            var response = await _mediator.Send(request);

            return response.IsSuccess
                ? Ok(response.Payload)
                : response.ToActionResult();
        }
    }
}
