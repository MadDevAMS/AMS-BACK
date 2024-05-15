using System.Net;
using AMS.Application.Commons.Bases;
using AMS.Application.UseCases.User.Command.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand cmd)
        {
            var repsonse = await _mediator.Send(cmd);
            return StatusCode(StatusCodes.Status200OK, repsonse);
        }
    }
}
