using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Entidades.Command.CreateEntidad
{
    public class CreateEntidadCommand : IRequest<BaseResponse<bool>>
    {
        public string Nombre { get; set; } = null!;
        public string RazonSocial { get; set; } = null!;
        public string RUC { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
