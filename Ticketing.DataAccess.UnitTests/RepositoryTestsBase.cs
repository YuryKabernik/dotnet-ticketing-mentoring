using Ticketing.DataAccess.UnitTests.Fixtures;

namespace Ticketing.DataAccess.UnitTests;

public abstract class RepositoryTestsBase : IClassFixture<LocalDbTestDatabaseFixture>, IDisposable
{
    protected readonly DataContext _context;
    private readonly LocalDbTestDatabaseFixture localDb;

    public RepositoryTestsBase(LocalDbTestDatabaseFixture localDb)
    {
        this.localDb = localDb;
        this._context = localDb.CreateContext();
        this.Seed(this._context);
    }

    public void Dispose()
    {
        this.localDb.Cleanup();
        this._context.Dispose();
    }

    protected virtual void Seed(DataContext context)
    {
        this._context.SaveChanges();
    }
}
