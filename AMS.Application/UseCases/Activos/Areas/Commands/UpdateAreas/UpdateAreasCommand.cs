using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using MediatR;

namespace AMS.Application.UseCases.Activos.Areas.Commands.UpdateAreas
{
    public class UpdateAreasCommand : IRequest<BaseResponse<AreaDto>>
    {
        public long IdArea { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
