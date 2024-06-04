using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.CreateMetricas
{
    public class CreateMetricasCommand : IRequest<BaseResponse<bool>>
    {
        public long IdPuntoMonitoreo { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Tipo { get; set; }
    }
}
