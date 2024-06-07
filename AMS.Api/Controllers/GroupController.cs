using System.Net;
using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Groups;
using AMS.Application.UseCases.Groups.Command.CreateGroup;
using AMS.Application.UseCases.Groups.Command.UpdateGroup;
using AMS.Application.UseCases.Groups.Queries.GetGroup;
using AMS.Application.UseCases.Groups.Queries.ListGroups;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Api.Controllers
{
    [Route("api/v{version:apiVersion}/")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
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

        [HttpPost, MapToApiVersion("1.0")]
        [Route("groups")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("group"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGroupById(
            [FromQuery] long idGroup
        )
        {
            var qry = new GetGroupQuery()
            {
                IdGroup = idGroup
            };
            var response = await _mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("groups"), MapToApiVersion("1.0")]
        [Authorize]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListGroups()
        {
            var qry = new ListGroupsQuery();
            var response = await _mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
