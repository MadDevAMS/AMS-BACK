using AMS.Api.Authentication;
using AMS.Api.Middleware;
using AMS.Application;
using AMS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
const string cors = "Cors";

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddAuthentication(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.AddMiddlewareValidation();

app.MapControllers();

app.Run();
