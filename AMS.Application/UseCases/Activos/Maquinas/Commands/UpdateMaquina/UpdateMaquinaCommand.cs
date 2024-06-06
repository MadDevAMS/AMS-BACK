using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Activos.Maquinas.Commands.UpdateMaquina
{
    public class UpdateMaquinaCommand : IRequest<BaseResponse<bool>>
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TipoMaquina { get; set; } = null!;
    }
}
