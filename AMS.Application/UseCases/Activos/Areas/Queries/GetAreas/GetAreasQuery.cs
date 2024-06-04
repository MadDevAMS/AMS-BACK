using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using MediatR;

namespace AMS.Application.UseCases.Activos.Areas.Queries.GetAreas
{
    public class GetAreasQuery : IRequest<BaseResponse<AreaResponseDto>>
    {
        public long Id { get; set; }
    }
}
