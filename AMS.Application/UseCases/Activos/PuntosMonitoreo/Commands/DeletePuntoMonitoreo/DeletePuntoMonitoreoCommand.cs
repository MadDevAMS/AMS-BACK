using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.DeletePuntoMonitoreo
{
    public class DeletePuntoMonitoreoCommand : IRequest<BaseResponse<bool>>
    {
        public long Id { get; set; }
    }
}
