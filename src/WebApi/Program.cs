using Ticketing.Application;
using Ticketing.DataAccess.DependencyInjection;
using Ticketing.WebApi.Caching;
using Ticketing.WebApi.Startup;
using Ticketing.WebApi.Startup.ExceptionHandlers;
using Ticketing.WebApi.Startup.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddResponseCaching();
builder.Services.AddControllers(options => options.AddCacheProfileEvents());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddExceptionHandler<DomainExceptionHandler>();

builder.Services.ConfigureOptions<SwaggerGenOptionsBuilder>();
builder.Services.AddSwaggerGen();

// Add application services
builder.Services.AddDataAccess();
builder.Services.AddApplication();

// Health checks
builder.Services.AddHealthChecks()
    .AddDataAccessDependenciesHealthCheck()
    .AddApplicationDependenciesHealthCheck();

if (builder.Environment.IsDevelopment())
{
    builder.Services.UseHealthCheckUIConfigured();
}

// Configure the HTTP request pipeline.
var endpoints = builder.Build();

// Ensure swagger generations are always generated
endpoints.UseSwagger();

if (endpoints.Environment.IsDevelopment())
{
    // exception page for developers
    endpoints.UseDeveloperExceptionPage();

    // swagger UI middleware
    endpoints.UseSwaggerUI();

    // health check UI middleware
    endpoints.UseHealthChecksUI(config => config.UIPath = "/healthchecks-ui");
}
else
{
    endpoints.UseExceptionHandler("/Error");
}

endpoints.UseHttpsRedirection();
endpoints.UseAuthorization();

endpoints.MapLivenessEndpoint();
endpoints.MapReadinessEndpoint();

endpoints.UseResponseCaching();
endpoints.MapControllers();

endpoints.Run();

namespace Ticketing.WebApi
{
    public partial class Program { }
}
