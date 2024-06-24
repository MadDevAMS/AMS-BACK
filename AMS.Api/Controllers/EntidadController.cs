using System.Net;
using AMS.Application;
using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Entidad;
using AMS.Application.UseCases.Entidades.Command.CreateEntidad;
using AMS.Application.UseCases.Entidades.Command.UpdateEntidad;
using AMS.Application.UseCases.Entidades.Queries.GetEntidad;
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

        [HttpGet("entidad"), MapToApiVersion("1.0")]
        [Authorize]
        [HasPermission(Permission.Admin)]
        [ProducesResponseType(typeof(BaseResponse<EntidadDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetEntidad()
        {
            var qry = new GetEntidadQuery();
            var response = await _mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut("entidades"), MapToApiVersion("1.0")]
        [Authorize]
        [HasPermission(Permission.Admin)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateEntidad([FromForm] UpdateEntidadCommand cmd)
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

        [HttpGet("entidad/archivos"), MapToApiVersion("1.0")]
        [HasPermission(Permission.ProcesarDatosExcel)]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<S3ObjectDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFilesEntidad()
        {
            var qry = new GetFilesMetricasQuery();
            var response = await _mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
