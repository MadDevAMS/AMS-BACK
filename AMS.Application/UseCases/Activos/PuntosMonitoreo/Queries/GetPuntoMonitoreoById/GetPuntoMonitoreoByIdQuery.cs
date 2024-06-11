using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using MediatR;

namespace AMS.Application.UseCases.Activos.PuntosMonitoreo.Queries.GetPuntoMonitoreoById
{
    public class GetPuntoMonitoreoByIdQuery : IRequest<BaseResponse<PuntoMonitoreoDto>>
    {
        public long Id { get; set; }
    }
}
