using AMS.Application.Commons.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.UseCases.Entidades.Command.UpdateEntidad
{
    public class UpdateEntidadCommand : IRequest<BaseResponse<bool>>
    {
        public IFormFile? File { get; set; }
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Direccion { get; set; } = null!;
    }
}
