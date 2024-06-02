using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Groups;
using MediatR;

namespace AMS.Application.UseCases.Groups.UpdateGroups
{
    public class UpdateGroupCommand : IRequest<BaseResponse<GroupsDto>>
    {
        public long GroupId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<long> Permissions { get; set; } = null!;
        public List<long> Users { get; set; } = null!;
        public int State { get; set; }
    }
}
