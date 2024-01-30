using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Ticketing.Application;
using Ticketing.DataAccess.DependencyInjection;
using Ticketing.WebApi.Caching;
using Ticketing.WebApi.Startup;
using Ticketing.WebApi.Startup.ExceptionHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddResponseCaching();
builder.Services.AddControllers(options => options.AddCacheProfileEvents());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddExceptionHandler<DomainExceptionHandler>();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerGenOptionsBuilder>();
builder.Services.AddSwaggerGen();

// Add application services
builder.Services.AddDataAccess();
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseResponseCaching();
app.MapControllers();

app.Run();

namespace Ticketing.WebApi
{
    public partial class Program { }
}
