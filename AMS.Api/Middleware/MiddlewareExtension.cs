namespace AMS.Api.Middleware
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder AddMiddlewareValidation(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ValidationMiddleware>();
        }
    }
}
