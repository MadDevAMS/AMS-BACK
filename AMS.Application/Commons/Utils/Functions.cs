using AMS.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace AMS.Application.Commons.Utils
{
    internal static class Functions
    {
        public static long? GetUserOrEntidadIdFromClaims(IHttpContextAccessor httpContextAccessor, string claims)
        {
            var idString = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == claims)?.Value;

            if (string.IsNullOrEmpty(idString))
            {
                return null;
            }

            if (long.TryParse(idString, out var id))
            {
                return id;
            }

            return null;
        }
    }
}
