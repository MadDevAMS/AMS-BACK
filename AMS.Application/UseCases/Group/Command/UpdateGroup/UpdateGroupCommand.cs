using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Group.Command.UpdateGruop
{
    public class UpdateGroupCommand : IRequest<BaseResponse<bool>>
    {
        public long Id { get; set; }    
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
