using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using MediatR;

namespace AMS.Application.UseCases.Activos.Maquinas.Commands.CreateMaquina
{
    public class CreateMaquinaCommand : IRequest<BaseResponse<MaquinaDto>>
    {
        public long IdArea { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TipoMaquina { get; set; } = null!;
    }
}
