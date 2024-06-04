using AMS.Application.Commons.Bases;
using AMS.Application.UseCases.GroupPermission.Command.CreateGroupPermissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AMS.Api.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupPermissionController : Controller
    {
        private readonly IMediator _mediator;
        
        public GroupPermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<bool>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateGroupPermission([FromBody] CreateGroupPermissionsCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK, response);
        } 
    }
}
