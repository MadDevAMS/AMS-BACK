﻿using System.Net;
using AMS.Application.Commons.Bases;
using AMS.Application.UseCases.User.Command.CreateUser;
using AMS.Application.UseCases.User.Command.Login;
using MediatR;
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
            var repsonse = await _mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, repsonse);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoginUser([FromBody] LoginCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
