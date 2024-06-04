using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Activos.Areas.Commands.DeleteAreas
{
    public class DeleteAreasCommand : IRequest<BaseResponse<bool>>
    {
        public long Id { get; set; }
    }
}
