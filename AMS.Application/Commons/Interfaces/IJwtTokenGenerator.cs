using AMS.Application.Dtos.Permissions;

namespace AMS.Application.Commons.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(UserDetailResponseDto user);
    }
}
