using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Permissions;
using System.Net;
using AMS.Application.UseCases.Permisos.Queries.ListPermissions;


namespace AMS.Api.Controllers
{

    [Route("api/v{version:apiVersion}")]
    [ApiController]
    public class PermissionController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("permissions"), MapToApiVersion("1.0")]
        [Authorize]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<PermissionsListResponseDto>>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> ListPermissions(
            [FromQuery] int numPage,
            [FromQuery] int records
        )
        {
            var qry = new ListPermissionQuery
            {
                NumPage = numPage,
                Records = records
            };
            var response = await _mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}