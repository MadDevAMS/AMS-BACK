using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace AMS.Api.Authentication
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var openApi = new OpenApiInfo
            {
                Title = "AMS API",
                Version = "v1",
                Description = "AMS dotnet api 2024",
                TermsOfService = new Uri("http://opensource.org/licenses/NIT"),
                Contact = new OpenApiContact
                {
                    Name = "madcat I.E.R.L",
                    Email = "adriano.gongora.juarez@tecsup.edu.pe",
                    Url = new Uri("https://ams.madcat.one")
                },
                License = new OpenApiLicense
                {
                    Name = "License",
                    Url = new Uri("http://opensource.org/licenses/NIT")
                }
            };

            services.AddSwaggerGen(x =>
            {
                openApi.Version = "v1";
                x.SwaggerDoc("v1", openApi);

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "JWT Bearer Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                x.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {securityScheme, new string[]{ } }
            });

            });

            return services;
        }
    }
}
