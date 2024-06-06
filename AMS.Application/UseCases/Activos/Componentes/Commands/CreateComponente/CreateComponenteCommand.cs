using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Activos.Componentes.Commands.CreateComponente
{
    public class CreateComponenteCommand : IRequest<BaseResponse<bool>>
    {
        public long IdMaquina { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Potencia { get; set; }
        public int Velocidad { get; set; }
    }
}
