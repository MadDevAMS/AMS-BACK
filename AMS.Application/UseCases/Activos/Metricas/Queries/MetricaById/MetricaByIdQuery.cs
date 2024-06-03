using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using MediatR;

namespace AMS.Application.UseCases.Activos.Metricas.Queries.MetricaById
{
    public class MetricaByIdQuery : IRequest<BaseResponse<MetricasResponseDto>>
    {
        public long Id { get; set; }
    }
}
