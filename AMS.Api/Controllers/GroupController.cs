using MediatR; // biblioteca que permite implementar un patro mediador.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AMS.Application.UseCases.Group.Command.CreateGroup;
using AMS.Application.Commons.Bases;
using System.Net;


namespace AMS.Controllers;

[Authorize] // atributo para uqe todos los controladores deban tener permiso.
[Route("api/[controller]")]  // ruta para probar el api
[ApiController]  // todos los tipos derivados se utilizan para servir respuestas de API HTTP
public class Group : ControllerBase
{
    private readonly IMediator _mediator; // enviar consultas 

    public Group(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupCommand cmd)
    {
        var response = await _mediator.Send(cmd);
        return Ok(response);
    }

}

// arriba ya estaba hecho por defecto (ejemplo)
