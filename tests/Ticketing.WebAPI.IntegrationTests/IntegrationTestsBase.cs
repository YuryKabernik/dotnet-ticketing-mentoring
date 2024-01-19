using Ticketing.DataAccess;
using Ticketing.WebApi.IntegrationTests.Fixtures;

namespace Ticketing.WebApi.IntegrationTests;

public class IntegrationTestsBase : IClassFixture<WebApplicationFixture>
{
    protected readonly HttpClient _client;
    private readonly WebApplicationFixture _factory;

    public IntegrationTestsBase(WebApplicationFixture applicationFixture)
    {
        this._factory = applicationFixture;
        this._client = applicationFixture.CreateClient();

        this.InitializeDatabase();
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
