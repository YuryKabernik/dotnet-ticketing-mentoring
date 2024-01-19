using Ticketing.WebApi.IntegrationTests.Fixtures;

namespace Ticketing.WebApi.IntegrationTests;

public abstract class IntegrationTestsBase : IAssemblyFixture<WebApplicationFixture>
{
    protected readonly HttpClient _client;
    protected readonly WebApplicationFixture _factory;

    public IntegrationTestsBase(WebApplicationFixture applicationFixture)
    {
        this._factory = applicationFixture;
        this._client = applicationFixture.CreateClient();
    }
}
