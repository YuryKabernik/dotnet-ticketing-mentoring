using Ticketing.Domain.Entities.Ordering;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Carting.BookCartSeats;

/// <summary>
/// Moves all the seats in the cart to a booked state.
/// </summary>
public class BookCartSeatsCommandHandler : ICommandHandler<BookCartSeatsCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;

    public BookCartSeatsCommandHandler(
        ICartRepository cartRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        this._cartRepository = cartRepository;
        this._orderRepository = orderRepository;
        this._unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(BookCartSeatsCommand command, CancellationToken cancellation)
    {
        var cart = await this._cartRepository.GetWithSeatsAsync(command.CartId, cancellation);
        NotFoundException.ThrowIfNull(cart);

        cart!.BookSeats();

        var order = Order.CreateFrom(cart!);
        await RegisterOrder(order, cancellation);

        cart!.Clear();

        await this._unitOfWork.SaveChanges(cancellation);
    }

    private async Task RegisterOrder(Order order, CancellationToken cancellation)
    {
        await this._orderRepository.Add(order, cancellation);
    }
}
