using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Activos.Areas.Commands.UpdateAreas
{
    public class UpdateAreasCommand : IRequest<BaseResponse<bool>>
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
