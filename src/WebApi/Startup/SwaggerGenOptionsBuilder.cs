using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ticketing.WebApi.Startup;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class SwaggerGenOptionsBuilder : IConfigureOptions<SwaggerGenOptions>
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="options"></param>
    public void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new()
        {
            Version = "v1",
            Title = "Event Ticketing API",
            Description = "An ASP.NET Core Web API for managing Ticketing domain"
        });

        this.IncludeXmlComments(options);
    }

    /// <summary>
    /// Inject human-friendly descriptions for Operations, Parameters and Schemas based on XML Comment files
    /// </summary>
    /// <param name="options"></param>
    private void IncludeXmlComments(SwaggerGenOptions options)
    {
        var containingAssemblyName = typeof(SwaggerGenOptionsBuilder).Assembly.GetName().Name;

        var xmlFilename = $"{containingAssemblyName}.xml";
        var xmlCommentsFileFullName = Path.Combine(AppContext.BaseDirectory, xmlFilename);

        if (xmlCommentsFileFullName is not null && Path.Exists(xmlCommentsFileFullName))
        {
            options.IncludeXmlComments(xmlCommentsFileFullName);
        }
    }
}
