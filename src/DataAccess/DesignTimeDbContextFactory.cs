using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace Ticketing.DataAccess;

/// <summary>
/// Implementation of the DbContext design-time factory for Ticketing Database migration via Entity Framework CLI.
/// More Info:
/// <see cref="https://learn.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#from-a-design-time-factory"/>
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>, IOptions<DatabaseSettings>
{
    public DatabaseSettings Value { get; } = new()
    {
        ConnectionString = "Server=localhost;Database=Ticketing;Trusted_Connection=True;TrustServerCertificate=true;User=TktUser;",
        TimeoutSeconds = 30,
        RetryAttempts = 3,
        RetryDelaySeconds = 5
    };

    public DataContext CreateDbContext(string[] args) => new(this);
}
