using AMS.Application.Commons.Bases;
using AMS.Application.UseCases.GroupUsers.Command.CreateGroupUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AMS.Api.Controllers
{
    [Route("api/v{version:apiVersion}/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class GroupUsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        [HttpPost("groupUsers"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> CreateGroupUser([FromBody] CreateGroupUsersCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK, response);

        }

    }
}
