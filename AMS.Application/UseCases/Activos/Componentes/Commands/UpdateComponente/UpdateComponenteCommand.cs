using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using MediatR;

namespace AMS.Application.UseCases.Activos.Componentes.Commands.UpdateComponente
{
    public class UpdateComponenteCommand : IRequest<BaseResponse<ComponenteDto>>
    {
        public long IdComponente { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Potencia { get; set; }
        public int Velocidad { get; set; }
    }
}
