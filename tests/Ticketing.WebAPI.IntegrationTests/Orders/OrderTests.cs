using System.Net;
using Ticketing.DataAccess;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Event;
using Ticketing.Tests.Core.DataSeeds;
using Ticketing.Tests.Core.DataSeeds.EventRelated;
using Ticketing.WebApi.IntegrationTests.Fixtures;
using Ticketing.WebApi.Models;
using Ticketing.WebApi.Orders.Models;

namespace Ticketing.WebApi.IntegrationTests.Orders;

public class OrderTests : IntegrationTestsBase
{
    private const string EndpointTemplate = "api/orders/carts/{0}";

    private Cart _testCart;

    public OrderTests(WebApplicationFixture webApplicationFactory)
        : base(webApplicationFactory)
    {
        this._testCart = this.InitializeDatabaseWithCart();
    }

    [Fact]
    public async Task GetCart_ByGuid_ReturnsValidCartInfo()
    {
        var uri = string.Format(EndpointTemplate, this._testCart.Guid);

        var cartInfo = await this._client.GetFromJsonAsync<CartInfo>(uri);

        Assert.NotNull(cartInfo);
        Assert.Equal(this._testCart.Guid, cartInfo.CartId);
        Assert.Equal(this._testCart.Seats.Count, cartInfo.Count);
        Assert.NotNull(cartInfo.Items);
    }

    [Fact]
    public async Task GetCart_ByInvalidGuid_Returns404NotFound()
    {
        var invalidGuid = Guid.NewGuid();
        var uri = string.Format(EndpointTemplate, invalidGuid);

        var result = await this._client.GetAsync(uri);
        var message = await result.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        Assert.Equal($"Can't find the requested resource (/{uri}).", message);
    }

    [Fact]
    public async Task PostCart_ValidSeat_AddsSeatToCart()
    {
        var seat = this.InitializeDatabaseWithSeat();
        var uri = string.Format(EndpointTemplate, this._testCart.Guid);
        var body = new SeatSelection(
            seat.Row.Section.Event.Id,
            seat.Id,
            seat.Price.Id
        );

        var result = await this._client.PostAsJsonAsync(uri, body);
        var cartInfo = await result.Content.ReadFromJsonAsync<CartInfo>();

        Assert.NotNull(cartInfo);
        Assert.Equal(this._testCart.Guid, cartInfo.CartId);
        Assert.Contains(cartInfo.Items, item => item.Id == seat.Id);

        int expectedSeatsInCart = this._testCart.Seats.Count + 1;
        Assert.Equal(expectedSeatsInCart, cartInfo.Count);
    }

    [Theory]
    [InlineData(1000, null, null)]
    [InlineData(null, 1000, null)]
    [InlineData(null, null, 1000)]
    public async Task PostCart_InvalidBody_Returns404NotFound(
        int? invalidEventId,
        int? invalidSeatId,
        int? invalidPriceId)
    {
        var seat = this.InitializeDatabaseWithSeat();
        var uri = string.Format(EndpointTemplate, this._testCart.Guid);
        var body = new SeatSelection(
            invalidEventId ?? seat.Row.Section.Event.Id,
            invalidSeatId ?? seat.Id,
            invalidPriceId ?? seat.Price.Id
        );

        var result = await this._client.PostAsJsonAsync(uri, body);
        var message = await result.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        Assert.Equal($"Can't find the requested resource (/{uri}).", message);
    }

    private Cart InitializeDatabaseWithCart()
    {
        using DataContext dataContext = this._factory.GetDbContext();
        var cart = CartDataSeed.Seed();

        dataContext.Add(cart);
        dataContext.SaveChanges();

        return cart;
    }

    private EventSeat InitializeDatabaseWithSeat()
    {
        using DataContext dataContext = this._factory.GetDbContext();
        var seat = EventSeatDataSeed.Seed();

        dataContext.Add(seat);
        dataContext.SaveChanges();

        return seat;
    }
}
