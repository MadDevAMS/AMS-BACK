using System.Net;
using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using AMS.Application.UseCases.Activos.Areas.Commands.CreateAreas;
using AMS.Application.UseCases.Activos.Areas.Commands.DeleteAreas;
using AMS.Application.UseCases.Activos.Areas.Commands.UpdateAreas;
using AMS.Application.UseCases.Activos.Areas.Queries.GetAreas;
using AMS.Application.UseCases.Activos.Componentes.Commands.CreateComponente;
using AMS.Application.UseCases.Activos.Componentes.Commands.DeleteComponente;
using AMS.Application.UseCases.Activos.Componentes.Commands.UpdateComponente;
using AMS.Application.UseCases.Activos.Componentes.Queries.GetComponente;
using AMS.Application.UseCases.Activos.Maquinas.Commands.CreateMaquina;
using AMS.Application.UseCases.Activos.Maquinas.Commands.DeleteMaquina;
using AMS.Application.UseCases.Activos.Maquinas.Commands.UpdateMaquina;
using AMS.Application.UseCases.Activos.Maquinas.Queries.GetMaquina;
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
        public async Task<IActionResult> GetMetricaById([FromQuery] long idMetrica)
        {
            var qry = new MetricaByIdQuery() { Id = idMetrica };
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
        public async Task<IActionResult> DeleteMetrica([FromQuery] DeleteMetricaCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("puntosMonitoreo"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<PuntoMonitoreoResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPuntoMonitoreoById([FromQuery] long idPunto)
        {
            var qry = new GetPuntoMonitoreoByIdQuery() { Id = idPunto };
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
        public async Task<IActionResult> DeletePuntoMonitoreo([FromQuery] DeletePuntoMonitoreoCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("componentes"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<ComponenteResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetComponenteById([FromQuery] long idComponente)
        {
            var qry = new GetComponenteQuery { Id = idComponente };
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
        public async Task<IActionResult> DeleteComponente([FromQuery] DeleteComponenteCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("maquinas"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<MaquinaResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMaquinaById([FromQuery] long idMaquina)
        {
            var qry = new GetMaquinaQuery() { Id = idMaquina };
            var response = await mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("maquinas"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateMaquina([FromBody] CreateMaquinaCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut("maquinas"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMaquina([FromBody] UpdateMaquinaCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpDelete("maquinas"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMaquina([FromQuery] DeleteMaquinaCommmand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("areas"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<MaquinaResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAreaById([FromQuery] long idArea)
        {
            var qry = new GetAreasQuery() { Id = idArea };
            var response = await mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("areas"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateArea([FromBody] CreateAreasCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut("areas"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateArea([FromBody] UpdateAreasCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpDelete("areas"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteArea([FromQuery] long idArea)
        {
            var cmd = new DeleteAreasCommand() { Id = idArea };
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
