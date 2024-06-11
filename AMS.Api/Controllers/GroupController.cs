using System.Net;
using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Groups;
using AMS.Application.Dtos.Permissions;
using AMS.Application.UseCases.Group.Command.DeleteGroup;
using AMS.Application.UseCases.Groups.Command.CreateGroup;
using AMS.Application.UseCases.Groups.Command.UpdateGroup;
using AMS.Application.UseCases.Groups.Queries.GetGroup;
using AMS.Application.UseCases.Groups.Queries.ListGroups;
using AMS.Application.UseCases.Permisos.Queries.ListPermissions;
using AMS.Infrastructure.Authentication.Permissions;
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

        [HttpPut("groups"), MapToApiVersion("1.0")]
        [Authorize]
        [HasPermission(Permission.Admin)]
        [ProducesResponseType(typeof(BaseResponse<GroupsDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpDelete("groups"), MapToApiVersion("1.0")]
        [Authorize]
        [HasPermission(Permission.Admin)]

        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteGroup([FromQuery] DeleteGroupCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("groups"), MapToApiVersion("1.0")]
        [Authorize]
        [HasPermission(Permission.Admin)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("group"), MapToApiVersion("1.0")]
        [Authorize]
        [HasPermission(Permission.Admin)]
        [ProducesResponseType(typeof(BaseResponse<GroupByIdDto>), (int)HttpStatusCode.OK)]
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
        [HasPermission(Permission.Admin)]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<GroupListDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListGroups(
            [FromQuery] int numPage,
            [FromQuery] int records
        )
        {
            var qry = new ListGroupsQuery()
            {
                NumPage = numPage,
                Records = records,
            };
            var response = await _mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("permissions"), MapToApiVersion("1.0")]
        [Authorize]
        [HasPermission(Permission.Admin)]
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
