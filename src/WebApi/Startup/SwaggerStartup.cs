using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ticketing.WebApi.Startup;

internal static class SwaggerStartup
{
    private static readonly string XmlCommentsFileFullName;
    private static readonly OpenApiInfo OpenWebApiInfo = new()
    {
        Version = "v1",
        Title = "Ticketing API",
        Description = "An ASP.NET Core Web API for managing Ticketing domain"
    };

    static SwaggerStartup()
    {
        string assemblyName = typeof(SwaggerStartup).Assembly.GetName().Name!;
        XmlCommentsFileFullName = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml");
    }

    public static void SetupSwaggerOptions(this SwaggerGenOptions options)
    {
        options.SwaggerDoc("Event Ticketing API", OpenWebApiInfo);

        if (XmlCommentsFileFullName is not null && Path.Exists(XmlCommentsFileFullName))
        {
            options.IncludeXmlComments(XmlCommentsFileFullName);
        }
    }
}
