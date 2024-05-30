using System.Net;
using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Groups;
using AMS.Application.UseCases.Groups.UpdateGroups;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Api.Controllers
{
    [Route("api/v{version:apiVersion}/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut, MapToApiVersion("1.0")]
        [Route("groups")]
        [ProducesResponseType(typeof(BaseResponse<GroupsDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
