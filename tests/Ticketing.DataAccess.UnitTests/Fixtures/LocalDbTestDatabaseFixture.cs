using Microsoft.Extensions.Options;

namespace Ticketing.DataAccess.UnitTests.Fixtures
{
    public class LocalDbTestDatabaseFixture
    {
        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        private readonly IOptions<DatabaseSettings> _settings = Options.Create<DatabaseSettings>(
            new()
            {
                ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Ticketing;Trusted_Connection=True;",
                TimeoutSeconds = 30,
                RetryAttempts = 3,
                RetryDelaySeconds = 5
            }
        );

        public LocalDbTestDatabaseFixture()
        {
            lock (_lock)
            {
                if (_databaseInitialized)
                    return;

                this.Cleanup();

                _databaseInitialized = true;
            }
        }

        public DataContext CreateContext() => new(this._settings);

        public void Cleanup()
        {
            using var context = this.CreateContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
