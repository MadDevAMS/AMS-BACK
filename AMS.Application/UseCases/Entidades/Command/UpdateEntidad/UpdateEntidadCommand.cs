using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Entidades.Command.UpdateEntidad
{
    public class UpdateEntidadCommand : IRequest<BaseResponse<bool>>
    {
        public long Id { get; set; }
        public string Image { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Direccion { get; set; } = null!;
    }
}
