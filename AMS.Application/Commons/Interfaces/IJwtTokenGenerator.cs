using AMS.Application.Dtos.User;

namespace AMS.Application.Commons.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(UserDetailResponseDto user);
    }
}
