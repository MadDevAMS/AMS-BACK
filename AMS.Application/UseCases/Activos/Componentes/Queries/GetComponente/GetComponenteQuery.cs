using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using MediatR;

namespace AMS.Application.UseCases.Activos.Componentes.Queries.GetComponente
{
    public class GetComponenteQuery : IRequest<BaseResponse<ComponenteDto>>
    {
        public long Id { get; set; }
    }
}
