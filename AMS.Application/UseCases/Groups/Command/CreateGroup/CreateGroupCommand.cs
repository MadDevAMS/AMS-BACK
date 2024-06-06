using AMS.Application.Commons.Bases;
using MediatR;

namespace AMS.Application.UseCases.Groups.Command.CreateGroup
{
    public class CreateGroupCommand : IRequest<BaseResponse<bool>>
    {
        public string Nombre { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<long> Permissions { get; set; } = null!;
        public List<long> Users { get; set; } = null!;
    }
}
