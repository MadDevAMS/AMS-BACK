using AMS.Application.Commons.Interfaces;
using AMS.Application.Interfaces.Persistence;
using AMS.Application.Interfaces.Services;
using AMS.Infrastructure.Authentication.Jwt;
using AMS.Infrastructure.Authentication.Permissions;
using AMS.Infrastructure.Persistence.Context;
using AMS.Infrastructure.Services;
using AMS.Infrastructure.Services.Excel;
using AMS.Infrastructure.Services.Excel.FormFile;
using AMS.Infrastructure.Services.S3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var assembly = typeof(ApplicationDbContext).Assembly.FullName;

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("AMSConnection"),
                    b => b.MigrationsAssembly(assembly)));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            services.AddSingleton<IS3Files, S3Files>();

            services.AddScoped<IExcelReader, ExcelReader>();
            services.AddScoped<AccelerationExcelReader>();
            services.AddScoped<VelocitExcelReader>();
            services.AddScoped<TemperatureExcelReader>();

            services.AddScoped<AccelarationExcelMemory>();
            services.AddScoped<VelocityExcelMemory>();
            services.AddScoped<TemperatureExcelMemory>();

            return services;
        }
    }
}
