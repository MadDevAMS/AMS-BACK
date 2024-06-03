using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Activos.Maquinas.Commands.DeleteMaquina
{
    public class DeleteMaquinaCommmand : IRequest<BaseResponse<bool>>
    {
        public long Id { get; set; }
    }
}
