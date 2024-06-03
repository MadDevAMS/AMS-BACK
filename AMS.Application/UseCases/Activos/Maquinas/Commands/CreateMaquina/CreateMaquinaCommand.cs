using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Activos.Maquinas.Commands.CreateMaquina
{
    public class CreateMaquinaCommand : IRequest<BaseResponse<bool>>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TipoMaquina { get; set; } = null!;
    }
}
