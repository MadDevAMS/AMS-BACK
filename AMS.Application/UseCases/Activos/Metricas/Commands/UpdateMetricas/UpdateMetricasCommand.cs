using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using MediatR;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.UpdateMetricas
{
    public class UpdateMetricasCommand : IRequest<BaseResponse<MetricasDto>>
    {
        public long IdMetrica { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Tipo { get; set; }

    }
}
