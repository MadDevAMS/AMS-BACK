using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Activos.Metricas.Commands.DeleteMetricas
{
    public class DeleteMetricaCommand : IRequest<BaseResponse<bool>>
    {
        public long Id { get; set; }
    }
}
