using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Ticketing.DataAccess.Setup;

/// <summary>
/// <inheritdoc/>
/// 
/// More info here: https://andrewlock.net/simplifying-dependency-injection-for-iconfigureoptions-with-the-configureoptions-helper/
/// 
/// </summary>
public class DatabaseConfigureOptions : IConfigureOptions<DatabaseSettings>
{
    private readonly IConfiguration _configuration;

    public DatabaseConfigureOptions(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public void Configure(DatabaseSettings options)
    {
        this._configuration.GetSection(DatabaseSettings.SectionName).Bind(options);
    }
}