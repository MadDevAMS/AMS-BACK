﻿using System.Net;
using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.User;
using AMS.Application.UseCases.User.Command.CreateUser;
using AMS.Application.UseCases.User.Command.DeleteUser;
using AMS.Application.UseCases.User.Command.Login;
using AMS.Application.UseCases.User.Queries.ListUsersEntidad;
using AMS.Application.UseCases.Users.Command.LoginAdmin;
using AMS.Application.UseCases.Users.Command.UpdateUser;
using AMS.Application.UseCases.Users.Queries.GetAuthUser;
using AMS.Infrastructure.Authentication.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Api.Controllers
{
    [Route("api/v{version:apiVersion}/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("users"), MapToApiVersion("1.0")]
        [Authorize]
        [HasPermission(Permission.Admin)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("login"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoginUser([FromBody] LoginCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet("users"), MapToApiVersion("1.0")]
        [Authorize]
        [HasPermission(Permission.Admin)]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<ListUsersResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListUsers(
            [FromQuery] string? userName,
            [FromQuery] string? userEmail,
            [FromQuery] int state,
            [FromQuery] DateTime dateCreated,
            [FromQuery] int numPage,
            [FromQuery] int records)
        {
            var qry = new ListUsersEntidadQuery
            {
                UserName = userName ?? string.Empty,
                UserEmail = userEmail ?? string.Empty,
                State = state,
                DateCreated = dateCreated,
                NumPage = numPage,
                Records = records
            };
            var response = await _mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }


        [HttpDelete("users"), MapToApiVersion("1.0")]
        [Authorize]
        [HasPermission(Permission.Admin)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("login/admin"), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoginUserAdmin([FromBody] LoginAdminCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPut("users"), MapToApiVersion("1.0")]
        [Authorize]
        [HasPermission(Permission.Admin)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommnad cmd)
        {
            var response = await _mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }
        
        [HttpGet("user"), MapToApiVersion("1.0")]
        [Authorize]
        [ProducesResponseType(typeof(BaseResponse<AuthUserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetInfoAuthUser()
        {
            var qry = new GetAuthUserQuery();
            var response = await _mediator.Send(qry);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
