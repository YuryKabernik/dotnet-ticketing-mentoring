using Ticketing.Application.CQRS;
using Ticketing.Domain.Entities;
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

    /// <summary>
    /// <see cref="MediatR"/> implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(BookCartSeatsCommand request, CancellationToken cancellationToken)
    {
        var cart = await this._cartRepository.GetWithSeatsAsync(request.CartId, cancellationToken);

        if (cart is null)
            throw new NotFoundException($"Cart {request.CartId} is not found.");

        cart.BookSeats();

        await this.SubmitOrder(cart, cancellationToken);

        cart.Clear();

        await this._unitOfWork.SaveChanges(cancellationToken);
    }

    private async Task SubmitOrder(Cart cart, CancellationToken cancellation)
    {
        var order = Order.CreateFrom(cart);

        await this._orderRepository.Add(order, cancellation);
    }
}