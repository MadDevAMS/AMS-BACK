using System.Net;
using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Permissions;
using AMS.Application.UseCases.User.Command.CreateUser;
using AMS.Application.UseCases.User.Command.Login;
using AMS.Application.UseCases.User.Queries.ListUsersEntidad;
using AMS.Infrastructure.Authentication.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoginUser([FromBody] LoginCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [Authorize]
        [HasPermission(Permission.ReadMember)]
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<ListUsersResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListUsers(
            [FromQuery] long idEntidad,
            [FromQuery] string? userName,
            [FromQuery] string? userEmail,
            [FromQuery] int state,
            [FromQuery] DateTime dateCreated,
            [FromQuery] int numPage,
            [FromQuery] int records)
        {
            var qry = new ListUsersEntidadQuery
            {
                IdEntidad = idEntidad,
                UserName = userName ?? string.Empty,
                UserEmail = userEmail ?? string.Empty,
                State = state,
                DateCreated = dateCreated,
                NumPage = numPage,
                Records = records
            };
            var repsonse = await _mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, repsonse);
        }

    }
}
