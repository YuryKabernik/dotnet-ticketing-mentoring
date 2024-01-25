using System.Net.Http.Json;
using Ticketing.DataAccess;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Event;
using Ticketing.Tests.Core.DataSeeds;
using Ticketing.Tests.Core.DataSeeds.EventRelated;
using Ticketing.WebApi.IntegrationTests.Fixtures;
using Ticketing.WebApi.Models;

namespace Ticketing.WebApi.LoadTests.Orders;

public class CartTests : IClassFixture<WebApplicationFixture>, IDisposable
{
    private const string EndpointTemplate = "api/orders/carts/{0}";

    private readonly int _clientRequests;
    private readonly WebApplicationFixture _factory;
    private readonly HttpClient _httpClient;

    public CartTests(WebApplicationFixture applicationFixture)
    {
        this._factory = applicationFixture;
        this._httpClient = applicationFixture.CreateClient();
        this._clientRequests = 10;
    }

    /// <summary>
    /// Consider reworking ‘POST orders/carts/{cart_id}’ endpoint to follow pessimistic concurrency strategy.
    /// Perform 1000 requests to ensure only 1 successful request is returned.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task PostCart_SubmitSelectedSeatForMultipleCarts_AddsSeatToSingleCartAsync()
    {
        int expectedConflictRequestsExceptSingleSuccessful = this._clientRequests - 1;
        var seat = this.InitializeDatabaseWithSeat();
        var carts = this.InitializeDatabaseWithCarts(this._clientRequests);

        HttpResponseMessage[] responses = await this.PostAsync(seat, carts);

        Assert.Single(responses, response => response.IsSuccessStatusCode);
        Assert.Equal(
            expectedConflictRequestsExceptSingleSuccessful,
            responses.Count(respone => respone.StatusCode.Equals(System.Net.HttpStatusCode.Conflict))
        );
    }

    private async Task<HttpResponseMessage[]> PostAsync(EventSeat seat, IEnumerable<Cart> carts)
    {
        var body = new SeatSelection(seat.Row.Section.Event.Id, seat.Id, seat.Price.Id);

        var tasks = carts.Select(cart =>
            this._httpClient.PostAsJsonAsync(string.Format(EndpointTemplate, cart.Guid), body)
        ).ToArray();

        return await Task.WhenAll(tasks);
    }

    private IEnumerable<Cart> InitializeDatabaseWithCarts(int cartsCount)
    {
        using DataContext dataContext = this._factory.GetDbContext();
        var carts = CartDataSeed.Seed(cartsCount);

        dataContext.AddRange(carts);
        dataContext.SaveChanges();

        return carts;
    }

    private EventSeat InitializeDatabaseWithSeat()
    {
        using DataContext dataContext = this._factory.GetDbContext();
        var seat = EventSeatDataSeed.Seed();

        dataContext.Add(seat);
        dataContext.SaveChanges();

        return seat;
    }

    public void Dispose()
    {
        this._httpClient.Dispose();
    }
}
