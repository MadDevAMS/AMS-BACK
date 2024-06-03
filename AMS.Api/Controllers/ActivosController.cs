using System.Net;
using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using AMS.Application.UseCases.Activos.Componentes.Commands.CreateComponente;
using AMS.Application.UseCases.Activos.Componentes.Commands.UpdateComponente;
using AMS.Application.UseCases.Activos.Componentes.Queries.GetComponente;
using AMS.Application.UseCases.Activos.Metricas.Commands.CreateMetricas;
using AMS.Application.UseCases.Activos.Metricas.Commands.DeleteMetricas;
using AMS.Application.UseCases.Activos.Metricas.Commands.UpdateMetricas;
using AMS.Application.UseCases.Activos.Metricas.Queries.MetricaById;
using AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.CreatePuntoMonitoreo;
using AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.DeletePuntoMonitoreo;
using AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.UpdatePuntoMonitoreo;
using AMS.Application.UseCases.Activos.PuntosMonitoreo.Queries.GetPuntoMonitoreoById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Api.Controllers
{

    [Route("api/v{version:apiVersion}/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ActivosController : ControllerBase
    {
        private readonly IMediator mediator;

        public ActivosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("metricas"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<MetricasResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMetricaById([FromQuery] MetricaByIdQuery qry)
        {
            var response = await mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("metricas"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateMetrica([FromBody] CreateMetricasCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut("metricas"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMetricca([FromBody] UpdateMetricasCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpDelete("metricas"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMetrica([FromBody] DeleteMetricaCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("puntosMonitoreo"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<PuntoMonitoreoResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPuntoMonitoreoById([FromQuery] GetPuntoMonitoreoByIdQuery qry)
        {
            var response = await mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("puntosMonitoreo"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreatePuntoMonitoreo([FromBody] CreatePuntoMonitoreoCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut("puntosMonitoreo"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdatePuntoMonitoreo([FromBody] UpdatePuntoMonitoreoCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpDelete("puntosMonitoreo"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeletePuntoMonitoreo([FromBody] DeletePuntoMonitoreoCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("componentes"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<ComponenteResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetComponenteById([FromQuery] GetComponenteQuery qry)
        {
            var response = await mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("componentes"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateComponente([FromBody] CreateComponenteCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut("componentes"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateComponente([FromBody] UpdateComponenteCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpDelete("componentes"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteComponente([FromBody] DeleteMetricaCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
