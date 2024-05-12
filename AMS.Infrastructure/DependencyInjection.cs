using AMS.Application.Interfaces.Persistence;
using AMS.Application.Interfaces.Services;
using AMS.Infrastructure.Persistence.Context;
using AMS.Infrastructure.Services;
using AMS.Infrastructure.Services.S3;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var assembly = typeof(ApplicationDbContext).Assembly.FullName;

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("AMSConnection"),
                    b => b.MigrationsAssembly(assembly)));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IS3Files, S3Files>();
            //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            //services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            //services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            //services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            //services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            return services;
        }
    }
}
