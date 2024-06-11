using System.Net;
using AMS.Application.Commons.Bases;
using AMS.Application.UseCases.Entidades.Command.CreateEntidad;
using AMS.Application.UseCases.Entidades.Command.UpdateEntidad;
using AMS.Infrastructure.Authentication.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Api.Controllers
{
    [Route("api/v{version:apiVersion}/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EntidadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EntidadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("entidades"), MapToApiVersion("1.0")]
        [Authorize]
        [HasPermission(Permission.Admin)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateEntidad([FromBody] UpdateEntidadCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("entidades"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateEntidad([FromBody] CreateEntidadCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
