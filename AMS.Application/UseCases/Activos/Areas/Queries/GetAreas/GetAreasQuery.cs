using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using MediatR;

namespace AMS.Application.UseCases.Activos.Areas.Queries.GetAreas
{
    public class GetAreasQuery : IRequest<BaseResponse<AreaDto>>
    {
        public long Id { get; set; }
    }
}
