using Ticketing.Application.Feature.Cart.Exception;
using Ticketing.Application.Feature.Cart.Request;
using Ticketing.Domain.Entities.Ordering;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application;

/// <summary>
/// Moves all the seats in the cart to a booked state.
/// </summary>
public class BookSeatsCommand : ICommandHandler<BookSeatsRequest>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ICartRepository cartRepository;
    private readonly IOrderRepository orderRepository;

    public BookSeatsCommand(
        ICartRepository cartRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork
        )
    {
        this.cartRepository = cartRepository;
        this.orderRepository = orderRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(BookSeatsRequest request, CancellationToken cancellation)
    {
        var cart = await this.cartRepository.GetWithSeatsAsync(request.CartId, cancellation);
        CartNotFoundExceptionException.ThrowIfNull(cart);

        cart!.BookSeats();

        var order = Order.CreateFrom(cart!);
        await RegisterOrder(order, cancellation);

        cart!.Clear();

        await this.unitOfWork.SaveChanges(cancellation);
    }

    private async Task RegisterOrder(Order order, CancellationToken cancellation)
    {
        await this.orderRepository.Add(order, cancellation);
    }
}
