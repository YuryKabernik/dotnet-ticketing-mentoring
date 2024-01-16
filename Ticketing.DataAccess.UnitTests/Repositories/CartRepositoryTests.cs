using Ticketing.DataAccess.Repositories;
using Ticketing.DataAccess.UnitTests.DataSeeds;
using Ticketing.DataAccess.UnitTests.Fixtures;
using Ticketing.Domain.Entities;

namespace Ticketing.DataAccess.UnitTests;

public class CartRepositoryTests : RepositoryTestsBase
{
    public static Guid CartId = Guid.NewGuid();
    private readonly CartRepository _cartRepository;

    public CartRepositoryTests(LocalDbTestDatabaseFixture databaseFixture) : base(databaseFixture)
    {
        this._cartRepository = new CartRepository(this._context);
    }

    [Fact]
    public async Task GetWithSeatsAsync_PriceNotZeroAndRowsAreExcluded()
    {
        Cart? result = await this._cartRepository.GetWithSeatsAsync(CartId, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEqual(0, result.FinalPrice);
        Assert.All(result.Seats, seat => Assert.NotNull(seat.Row));
    }

    [Fact]
    public async Task GetWithSeatsEventsAsync_FinalPriceNotZeroAndEventsAreIncluded()
    {
        Cart? result = await this._cartRepository.GetWithSeatsEventsAsync(CartId, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEqual(0, result.FinalPrice);
        Assert.All(result.Seats, seat => Assert.NotNull(seat.Row.Section.Event));
    }

    [Fact]
    public async Task GetAsync_FinalPriceIsZeroAndNoRelatedIncluded()
    {
        Cart? result = await this._cartRepository.GetAsync(CartId, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(0, result.FinalPrice);
        Assert.Empty(result.Seats);
    }

    [Fact]
    public async Task ListAsync_SimplyReturnsAllAvailableCarts()
    {
        var result = await this._cartRepository.ListAsync(CancellationToken.None);

        Assert.Single(result);
    }

    protected override void Seed(DataContext context)
    {
        this.SeedCart(context);
        base.Seed(context);
    }

    private void SeedCart(DataContext context)
    {
        Cart cart = CartDataSeed.Seed();
        cart.Guid = CartId;

        context.Add(cart);
    }
}