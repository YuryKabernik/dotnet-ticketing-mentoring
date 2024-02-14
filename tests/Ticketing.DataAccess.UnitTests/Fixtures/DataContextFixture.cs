using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Ticketing.DataAccess.Setup;

namespace Ticketing.DataAccess.UnitTests.Fixtures;

public class DataContextFixture : DataContext
{
    public DataContextFixture(IOptions<DatabaseSettings> settings) : base(settings)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("Ticketing")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
    }
}
