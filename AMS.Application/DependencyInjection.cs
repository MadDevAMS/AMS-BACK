using System.Reflection;
using AMS.Application.Commons.Behavoiur;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavoiur<,>));

            return services;
        }
    }
}
