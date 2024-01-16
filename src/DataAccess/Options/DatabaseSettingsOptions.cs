using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Ticketing.DataAccess.Options;

public class DatabaseSettingsOptions : IConfigureOptions<DatabaseSettings>
{
    private readonly IConfiguration _configuration;

    public DatabaseSettingsOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(DatabaseSettings options)
    {
        this._configuration.GetSection(DatabaseSettings.SectionName).Bind(options);
    }
}