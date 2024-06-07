using AMS.Infrastructure.Authentication.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Permissions;
using AMS.Application.Dtos.Roles;
using System.Net;
using AMS.Application.UseCases.Permisos.Queries.ListPermissions;


namespace AMS.Api.Controllers
{

    [Route("api/v{version:apiVersion}")]
    [ApiController]
    public class PermissionController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        
        [HttpGet("permission")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<PermissionsListResponseDto>>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> ListPermissions ()
        {
            var qry = new ListPermissionQuery
            {
                
            };
            var response = await _mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
