using AMS.Application.Commons.Bases;
using AMS.Application.Commons.Utils;
using Newtonsoft.Json;

namespace AMS.Api.Middleware
{
    public class CustomAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status403Forbidden && !context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                BaseResponse<string> response = new()
                {
                    Status = StatusCodes.Status403Forbidden,
                    Message = MiddlewareMessage.NOT_AUTHORIZATION
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }

}
