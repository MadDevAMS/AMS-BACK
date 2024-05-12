using AMS.Infrastructure.Commons.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace AMS.Infrastructure.Authentication.Permissions
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            string? memberId = context.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if (!Guid.TryParse(memberId, out Guid parsedMemberId))
            {
                return;
            }

            using var scope = _serviceScopeFactory.CreateScope();

            var permissions = context.User.Claims
                .Where(x => x.Type == CustomClaims.Permissions)
                .Select(x => x.Value)
                .ToHashSet();

            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
