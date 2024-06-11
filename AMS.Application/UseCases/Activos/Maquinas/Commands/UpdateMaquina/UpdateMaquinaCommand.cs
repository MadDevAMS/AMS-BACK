using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using MediatR;

namespace AMS.Application.UseCases.Activos.Maquinas.Commands.UpdateMaquina
{
    public class UpdateMaquinaCommand : IRequest<BaseResponse<MaquinaDto>>
    {
        public long IdMaquina { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TipoMaquina { get; set; } = null!;
    }
}
