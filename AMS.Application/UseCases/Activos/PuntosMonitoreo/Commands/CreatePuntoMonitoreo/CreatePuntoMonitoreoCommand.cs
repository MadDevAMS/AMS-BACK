using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Activos.PuntosMonitoreo.Commands.CreatePuntoMonitoreo
{
    public class CreatePuntoMonitoreoCommand : IRequest<BaseResponse<bool>>
    {
        public long IdComponente { get; set; }
        public string Description { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public string DireccionMedicion { get; set; } = null!;
        public string AnguloDireccion { get; set; } = null!;
        public string DatosMedicion { get; set; } = null!;
    }
}
