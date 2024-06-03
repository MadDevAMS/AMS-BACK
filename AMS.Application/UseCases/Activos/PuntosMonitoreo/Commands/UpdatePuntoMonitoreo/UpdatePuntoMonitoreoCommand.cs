using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.UpdatePuntoMonitoreo
{
    public class UpdatePuntoMonitoreoCommand : IRequest<BaseResponse<bool>>
    {
        public long Id { get; set; }
        public string Description { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public string DireccionMedicion { get; set; } = null!;
        public string AnguloDireccion { get; set; } = null!;
        public string DatosMedicion { get; set; } = null!;
    }
}
