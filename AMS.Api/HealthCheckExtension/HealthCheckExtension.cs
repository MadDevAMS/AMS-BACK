namespace AMS.Api.HealthCheckExtension
{
    public static class HealthCheckExtension
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("AMSConnection")!, tags: ["database"]);

            services.AddHealthChecksUI()
                .AddInMemoryStorage();

            return services;
        }
    }
}
