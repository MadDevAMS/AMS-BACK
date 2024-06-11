using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using MediatR;

namespace AMS.Application.UseCases.Activos.Maquinas.Queries.GetMaquina
{
    public class GetMaquinaQuery : IRequest<BaseResponse<MaquinaDto>> 
    {
        public long Id { get; set; }
    }
}
