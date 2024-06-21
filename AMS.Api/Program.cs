using AMS.Api.Authentication;
using AMS.Api.HealthCheckExtension;
using AMS.Api.Middleware;
using AMS.Application;
using AMS.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
const string cors = "Cors";

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddAuthentication(builder.Configuration)
    .AddHealthCheck(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioning();
builder.Services.AddSwagger();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: cors,
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.WithOrigins("*");
            corsPolicyBuilder.AllowAnyMethod();
            corsPolicyBuilder.AllowAnyHeader();
        });
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors(cors);

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<CustomAuthorizationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.AddMiddlewareValidation();

app.MapControllers();

app.MapHealthChecksUI();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

public partial class Program { }
