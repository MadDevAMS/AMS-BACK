using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AMS.Application.Commons.Interfaces;
using AMS.Application.Dtos.User;
using AMS.Infrastructure.Commons.Commons;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AMS.Infrastructure.Authentication.Jwt
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
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
                new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new(JwtRegisteredClaimNames.GivenName, user.Name),
                new(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new(CustomClaims.Entidad, user.IdEntidad.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claims.AddRange(user.Permissions.Select(idPermissions =>
                new Claim(CustomClaims.Permissions, idPermissions.ToString())));

            claims.AddRange(user.GroupNames.Select(groups =>
                new Claim(CustomClaims.Groups, groups)));

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
