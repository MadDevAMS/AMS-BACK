using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Activos.Componentes.Commands.DeleteComponente
{
    public class DeleteComponenteCommand : IRequest<BaseResponse<bool>>
    {
        public long Id { get; set; }
    }
}
