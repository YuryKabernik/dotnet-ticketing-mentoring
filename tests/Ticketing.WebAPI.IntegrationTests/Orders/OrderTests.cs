using System.Net;
using Ticketing.DataAccess;
using Ticketing.Domain.Entities;
using Ticketing.Tests.Core.DataSeeds;
using Ticketing.WebApi.Orders.Models;
using Ticketing.WebAPI.IntegrationTests.Fixtures;

namespace Ticketing.WebAPI.IntegrationTests.Orders
{
    public class OrderTests : IClassFixture<WebApplicationFixture>
    {
        private readonly WebApplicationFixture _factory;
        private readonly HttpClient _client;
        private Cart _testCart;

        public OrderTests(WebApplicationFixture webApplicationFactory)
        {
            this._factory = webApplicationFactory;
            this._client = this._factory.CreateClient();
            this._testCart = this.ReinitializeDatabase();
        }

        [Fact]
        public async Task GetCart_ByGuid_ReturnsValidCartInfo()
        {
            var result = await this._client.GetFromJsonAsync<CartInfo>($"api/orders/carts/{this._testCart.Guid}");

            Assert.NotNull(result);
            Assert.Equal(this._testCart.Guid, result.CartId);
            Assert.Equal(this._testCart.Seats.Count, result.Count);
            Assert.NotNull(result.Items);
        }

        [Fact]
        public async Task GetCart_ByInvalidGuid_Returns404NotFound()
        {
            var invalidGuid = Guid.NewGuid();

            var result = await this._client.GetAsync($"api/orders/carts/{invalidGuid}");

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        private Cart ReinitializeDatabase()
        {
            using var scope = this._factory.Services.CreateScope();

            var scopedServices = scope.ServiceProvider;
            var dataContext = scopedServices.GetRequiredService<DataContext>();

            dataContext.Database.EnsureDeleted();
            dataContext.Database.EnsureCreated();

            var cart = CartDataSeed.Seed();

            dataContext.Add(cart);
            dataContext.SaveChanges();

            return cart;
        }
    }
}
