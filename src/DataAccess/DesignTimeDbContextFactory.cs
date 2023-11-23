using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Ticketing.DataAccess;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("connectionsettings.json")
            .AddEnvironmentVariables()
            .Build();

        DatabaseSettings settings = builder
            .GetRequiredSection(DatabaseSettings.SectionName)
            .Get<DatabaseSettings>()!;

        return new DataContext(settings);
    }
}
