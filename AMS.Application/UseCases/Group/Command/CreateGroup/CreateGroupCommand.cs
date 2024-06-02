using MediatR;
using AMS.Application.Commons.Bases;
using AMS.Application.Dtos.Groups;

namespace AMS.Application.UseCases.Group.Command.CreateGroup;
public class CreateGroupCommand : IRequest<BaseResponse<GroupsDto>>
{
    public string Description { get; set; } = null!;
    public string Name { get; set; } = null!;
    public List<long> Permissions { get; set; } = null!;
    public List<long> Users { get; set; } = null!;
    public int State { get; set; }
}

