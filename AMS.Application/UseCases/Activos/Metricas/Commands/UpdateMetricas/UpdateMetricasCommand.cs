using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.UpdateMetricas
{
    public class UpdateMetricasCommand : IRequest<BaseResponse<bool>>
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Tipo { get; set; }

    }
}
