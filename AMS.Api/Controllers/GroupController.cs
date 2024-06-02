using MediatR; // biblioteca que permite implementar un patro mediador.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AMS.Application.Commons.Bases;
using System.Net;
using AMS.Application.UseCases.Group.Command.DeleteGruop;

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


    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]

    public async Task<IActionResult> DeleteGroup(long id)
    {
        var command = new DeleteGroupCommand { Id = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }



}

// arriba ya estaba hecho por defecto (ejemplo)