using System.Net;
using AMS.Application;
using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using AMS.Application.Dtos.Excel;
using AMS.Application.UseCases.Activos.Areas.Commands.CreateAreas;
using AMS.Application.UseCases.Activos.Areas.Commands.DeleteAreas;
using AMS.Application.UseCases.Activos.Areas.Commands.UpdateAreas;
using AMS.Application.UseCases.Activos.Areas.Queries.GetAreas;
using AMS.Application.UseCases.Activos.Componentes.Commands.CreateComponente;
using AMS.Application.UseCases.Activos.Componentes.Commands.DeleteComponente;
using AMS.Application.UseCases.Activos.Componentes.Commands.UpdateComponente;
using AMS.Application.UseCases.Activos.Componentes.Queries.GetComponente;
using AMS.Application.UseCases.Activos.Folder.Queries.GetFolderEntidad;
using AMS.Application.UseCases.Activos.Maquinas.Commands.CreateMaquina;
using AMS.Application.UseCases.Activos.Maquinas.Commands.DeleteMaquina;
using AMS.Application.UseCases.Activos.Maquinas.Commands.UpdateMaquina;
using AMS.Application.UseCases.Activos.Maquinas.Queries.GetMaquina;
using AMS.Application.UseCases.Activos.Metricas.Commands.ConvertAcceleration;
using AMS.Application.UseCases.Activos.Metricas.Commands.DeleteMetricas;
using AMS.Application.UseCases.Activos.Metricas.Commands.TemperatureExcelData;
using AMS.Application.UseCases.Activos.Metricas.Commands.VelocityExcelData;
using AMS.Application.UseCases.Activos.Metricas.Queries.MetricaById;
using AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.CreatePuntoMonitoreo;
using AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.DeletePuntoMonitoreo;
using AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.UpdatePuntoMonitoreo;
using AMS.Application.UseCases.Activos.PuntosMonitoreo.Queries.GetPuntoMonitoreoById;
using AMS.Infrastructure.Authentication.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Api.Controllers
{

    [Route("api/v{version:apiVersion}/")]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    public class ActivosController : ControllerBase
    {
        private readonly IMediator mediator;

        public ActivosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("folder")]
        [HasPermission(Permission.LeerFolder)]
        [ProducesResponseType(typeof(BaseResponse<FolderResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFolderEntidad()
        {
            var qry = new GetFolderEntidadQuery();
            var response = await mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("metricas"), MapToApiVersion("1.0")]
        [HasPermission(Permission.LeerMetricas)]
        [ProducesResponseType(typeof(BaseResponse<MetricasDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMetricaById([FromQuery] long idMetrica)
        {
            var qry = new MetricaByIdQuery() { Id = idMetrica };
            var response = await mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("metricas"), MapToApiVersion("1.0")]
        [HasPermission(Permission.CrearMetricas)]
        [ProducesResponseType(typeof(BaseResponse<MetricasDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateMetrica([FromBody] VelocityExcelCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut("metricas"), MapToApiVersion("1.0")]
        [HasPermission(Permission.ActualizarMetricas)]
        [ProducesResponseType(typeof(BaseResponse<MetricasDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMetricca([FromBody] TemperaturaExcelCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpDelete("metricas"), MapToApiVersion("1.0")]
        [HasPermission(Permission.EliminarMetricas)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMetrica([FromQuery] DeleteMetricaCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("puntosMonitoreo"), MapToApiVersion("1.0")]
        [HasPermission(Permission.LeerPuntos)]
        [ProducesResponseType(typeof(BaseResponse<PuntoMonitoreoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPuntoMonitoreoById([FromQuery] long idPunto)
        {
            var qry = new GetPuntoMonitoreoByIdQuery() { Id = idPunto };
            var response = await mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("puntosMonitoreo"), MapToApiVersion("1.0")]
        [HasPermission(Permission.CrearPuntos)]
        [ProducesResponseType(typeof(BaseResponse<PuntoMonitoreoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreatePuntoMonitoreo([FromBody] CreatePuntoMonitoreoCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut("puntosMonitoreo"), MapToApiVersion("1.0")]
        [HasPermission(Permission.ActualizarPuntos)]
        [ProducesResponseType(typeof(BaseResponse<PuntoMonitoreoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdatePuntoMonitoreo([FromBody] UpdatePuntoMonitoreoCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpDelete("puntosMonitoreo"), MapToApiVersion("1.0")]
        [HasPermission(Permission.EliminarPuntos)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeletePuntoMonitoreo([FromQuery] DeletePuntoMonitoreoCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("componentes"), MapToApiVersion("1.0")]
        [HasPermission(Permission.LeerComponentes)]
        [ProducesResponseType(typeof(BaseResponse<ComponenteDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetComponenteById([FromQuery] long idComponente)
        {
            var qry = new GetComponenteQuery { Id = idComponente };
            var response = await mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("componentes"), MapToApiVersion("1.0")]
        [HasPermission(Permission.CrearComponentes)]
        [ProducesResponseType(typeof(BaseResponse<ComponenteDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateComponente([FromBody] CreateComponenteCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut("componentes"), MapToApiVersion("1.0")]
        [HasPermission(Permission.ActualizarComponentes)]
        [ProducesResponseType(typeof(BaseResponse<ComponenteDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateComponente([FromBody] UpdateComponenteCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpDelete("componentes"), MapToApiVersion("1.0")]
        [HasPermission(Permission.EliminarComponentes)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteComponente([FromQuery] DeleteComponenteCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("maquinas"), MapToApiVersion("1.0")]
        [HasPermission(Permission.LeerMaquinas)]
        [ProducesResponseType(typeof(BaseResponse<MaquinaDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMaquinaById([FromQuery] long idMaquina)
        {
            var qry = new GetMaquinaQuery() { Id = idMaquina };
            var response = await mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("maquinas"), MapToApiVersion("1.0")]
        [HasPermission(Permission.CrearMaquinas)]
        [ProducesResponseType(typeof(BaseResponse<MaquinaDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateMaquina([FromBody] CreateMaquinaCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut("maquinas"), MapToApiVersion("1.0")]
        [HasPermission(Permission.ActualizarMaquinas)]
        [ProducesResponseType(typeof(BaseResponse<MaquinaDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMaquina([FromBody] UpdateMaquinaCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpDelete("maquinas"), MapToApiVersion("1.0")]
        [HasPermission(Permission.EliminarMaquinas)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMaquina([FromQuery] DeleteMaquinaCommmand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("areas"), MapToApiVersion("1.0")]
        [HasPermission(Permission.LeerArea)]
        [ProducesResponseType(typeof(BaseResponse<AreaDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAreaById([FromQuery] long idArea)
        {
            var qry = new GetAreasQuery() { Id = idArea };
            var response = await mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("areas"), MapToApiVersion("1.0")]
        [HasPermission(Permission.CrearArea)]
        [ProducesResponseType(typeof(BaseResponse<AreaDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateArea([FromBody] CreateAreasCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut("areas"), MapToApiVersion("1.0")]
        [HasPermission(Permission.ActualizarArea)]
        [ProducesResponseType(typeof(BaseResponse<AreaDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateArea([FromBody] UpdateAreasCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpDelete("areas"), MapToApiVersion("1.0")]
        [HasPermission(Permission.EliminarArea)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteArea([FromQuery] long idArea)
        {
            var cmd = new DeleteAreasCommand() { Id = idArea };
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("metricas/aceleracion"), MapToApiVersion("1.0")]
        [HasPermission(Permission.ProcesarDatosExcel)]
        [ProducesResponseType(typeof(BaseResponse<AccelerationExcelResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AccelerationData([FromForm] AcceleratioExcelCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("metricas/velocidad"), MapToApiVersion("1.0")]
        [HasPermission(Permission.ProcesarDatosExcel)]
        [ProducesResponseType(typeof(BaseResponse<VelocityExcelResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> VelocidadData([FromForm] VelocityExcelCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("metricas/temperatura"), MapToApiVersion("1.0")]
        [HasPermission(Permission.ProcesarDatosExcel)]
        [ProducesResponseType(typeof(BaseResponse<TemperatureExcelResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> TemperaturaData([FromForm] TemperaturaExcelCommand cmd)
        {
            var response = await mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("metricas/s3/acceleration"), MapToApiVersion("1.0")]
        [HasPermission(Permission.ProcesarDatosExcel)]
        [ProducesResponseType(typeof(BaseResponse<TemperatureExcelResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAccelerationDataS3([FromForm] GetAccelerationS3Query qry)
        {
            var response = await mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("metricas/s3/velocity"), MapToApiVersion("1.0")]
        [HasPermission(Permission.ProcesarDatosExcel)]
        [ProducesResponseType(typeof(BaseResponse<TemperatureExcelResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetVelocityDataS3([FromForm] GetVelocityS3Query qry)
        {
            var response = await mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("metricas/s3/temperatura"), MapToApiVersion("1.0")]
        [HasPermission(Permission.ProcesarDatosExcel)]
        [ProducesResponseType(typeof(BaseResponse<TemperatureExcelResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTemperatureDataS3([FromForm] GetTemperatureS3Query qry)
        {
            var response = await mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

    }
}
