using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AMS.Application.Commons.Interfaces;
using AMS.Application.Dtos.User;
using AMS.Infrastructure.Commons.Commons;
using Microsoft.IdentityModel.Tokens;

namespace AMS.Infrastructure.Authentication.Jwt
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateToken(UserDetailResponseDto user)
        {
            var signingCredentials = new SigningCredentials(
               new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                   SecurityAlgorithms.HmacSha256
            );


            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.GivenName, user.Name),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claims.AddRange(user.PermissionIds.Select(idPermissions =>
                new Claim(CustomClaims.Permissions, idPermissions.ToString())));

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
