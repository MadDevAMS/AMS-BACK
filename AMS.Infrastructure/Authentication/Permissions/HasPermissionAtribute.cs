using Microsoft.AspNetCore.Authorization;

namespace AMS.Infrastructure.Authentication.Permissions
{
    public sealed class HasPermissionAttribute(Permission permission) :
    AuthorizeAttribute(policy: permission.ToString());
}
