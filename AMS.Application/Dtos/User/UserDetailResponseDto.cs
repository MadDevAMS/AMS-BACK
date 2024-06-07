namespace AMS.Application.Dtos.User
{
    public class UserDetailResponseDto
    {
        public long UserId { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public long IdEntidad { get; set; }
        public List<string> Permissions { get; set; } = null!;
        public List<string> GroupNames { get; set; } = null!;
    }
}
