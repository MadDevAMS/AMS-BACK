using AMS.Application.Dtos.Groups;
using AMS.Application.Dtos.Roles;

namespace AMS.Application.Dtos.User
{
    public class ListUsersResponseDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int State {  get; set; }
        public DateTime AuditCreateDate {  get; set; }
        public List<PermissionsListResponseDto> Permissions { get; set; }
        public List<GroupListResponseDto> Group {  get; set; }
    }
}
