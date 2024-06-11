using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Activos;
using MediatR;

namespace AMS.Application.UseCases.Activos.Areas.Commands.CreateAreas
{
    public class CreateAreasCommand : IRequest<BaseResponse<AreaDto>>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public long IdParent { get; set; }
        public long IdEntidad { get; set; }
    }
}
