namespace AMS.Application.Dtos.User
{
    public class CreateUserDto
    {
        public long Id { get; set; }
        public long IdEntidad { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public int State {  get; set; }
        public List<long> Groups { get; set; } = null!;
    }
}
