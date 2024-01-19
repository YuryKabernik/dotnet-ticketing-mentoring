using System.Net;
using System.Net.Http.Headers;
using Ticketing.DataAccess;
using Ticketing.Domain.Entities.Event;
using Ticketing.Tests.Core.DataSeeds.EventRelated;
using Ticketing.WebApi.IntegrationTests.Fixtures;

namespace Ticketing.WebApi.IntegrationTests.Events;

public class EventsTests : IntegrationTestsBase
{
    private const string ResourceRoot = "api/events";

    public EventsTests(WebApplicationFixture applicationFixture)
        : base(applicationFixture)
    {
    }

    [Fact]
    public async Task GetEvents_ResponseCacheControl_PrivateMaxAge15()
    {
        CacheControlHeaderValue expectedCacheControl = new()
        {
            MaxAge = TimeSpan.FromSeconds(15),
            Private = true
        };

        var serverResult = await this._client.GetAsync(ResourceRoot);

        Assert.Equal(HttpStatusCode.OK, serverResult.StatusCode);
        Assert.Equal(expectedCacheControl, serverResult.Headers.CacheControl);
    }

    [Fact]
    public async Task GetSeats_ResponseCacheControl_PrivateMaxAge15()
    {
        var eventSeat = this.InitializeDatabaseWithEventSeats();
        var eventId = eventSeat.Row.Section.Event.Id;
        var sectionId = eventSeat.Row.Section.Id;

        var resourcePath = $"{ResourceRoot}/{eventId}/sections/{sectionId}/seats";

        CacheControlHeaderValue expectedCacheControl = new()
        {
            MaxAge = TimeSpan.FromSeconds(15),
            Private = true
        };

        var serverResult = await this._client.GetAsync(resourcePath);

        Assert.Equal(HttpStatusCode.OK, serverResult.StatusCode);
        Assert.Equal(expectedCacheControl, serverResult.Headers.CacheControl);
    }

    private EventSeat InitializeDatabaseWithEventSeats()
    {
        using DataContext dataContext = this.GetDbContext();
        var eventSeat = EventSeatDataSeed.Seed();

        dataContext.Add(eventSeat);
        dataContext.SaveChanges();

        return eventSeat;
    }
}
