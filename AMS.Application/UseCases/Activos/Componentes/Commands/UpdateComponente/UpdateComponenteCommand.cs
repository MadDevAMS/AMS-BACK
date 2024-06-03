using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Activos.Componentes.Commands.UpdateComponente
{
    public class UpdateComponenteCommand : IRequest<BaseResponse<bool>>
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Potencia { get; set; }
        public int Velocidad { get; set; }
    }
}
