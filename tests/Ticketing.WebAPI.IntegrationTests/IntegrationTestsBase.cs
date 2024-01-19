using Ticketing.DataAccess;
using Ticketing.WebApi.IntegrationTests.Fixtures;

namespace Ticketing.WebApi.IntegrationTests;

public class IntegrationTestsBase : IClassFixture<WebApplicationFixture>
{
    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    protected readonly HttpClient _client;
    private readonly WebApplicationFixture _factory;

    public IntegrationTestsBase(WebApplicationFixture applicationFixture)
    {
        this._factory = applicationFixture;
        this._client = applicationFixture.CreateClient();

        lock (_lock)
        {
            if (_databaseInitialized)
                return;

            this.InitializeDatabase();

            _databaseInitialized = true;
        }
    }

    protected void InitializeDatabase()
    {
        using DataContext dataContext = this.GetDbContext();

        dataContext.Database.EnsureDeleted();
        dataContext.Database.EnsureCreated();
    }

    protected DataContext GetDbContext()
    {
        IServiceScope scope = this._factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;

        return scopedServices.GetRequiredService<DataContext>();
    }
}
