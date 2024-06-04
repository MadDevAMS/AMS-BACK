using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Activos.Areas.Commands.CreateAreas
{
    public class CreateAreasCommand : IRequest<BaseResponse<bool>>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public long IdParent { get; set; }
        public long IdEntidad { get; set; }
    }
}
