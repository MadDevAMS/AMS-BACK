using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using MediatR;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.CreateMetricas
{
    public class CreateMetricasCommand : IRequest<BaseResponse<MetricasDto>>
    {
        public long IdPuntoMonitoreo { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Tipo { get; set; }
    }
}
