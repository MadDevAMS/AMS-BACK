namespace AMS.Application.Dtos.User
{
    public class AuthUserDto
    {
        public long UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<string> Permissions { get; set; } = null!;
        public List<string> GroupNames { get; set; } = null!;
    }
}
