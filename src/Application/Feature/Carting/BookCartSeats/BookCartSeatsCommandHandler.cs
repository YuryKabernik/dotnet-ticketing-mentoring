using MediatR;
using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Carting.BookCartSeats.Notifications;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Ordering;
using Ticketing.Domain.Entities.Payments;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Carting.BookCartSeats;

/// <summary>
/// Moves all the seats in the cart to a booked state.
/// </summary>
public class BookCartSeatsCommandHandler : ICommandHandler<BookCartSeatsCommand, BookCartSeatsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IPublisher _publisher;

    public BookCartSeatsCommandHandler(
        ICartRepository cartRepository,
        IOrderRepository orderRepository,
        IPublisher publisher,
        IUnitOfWork unitOfWork)
    {
        this._cartRepository = cartRepository;
        this._orderRepository = orderRepository;
        this._unitOfWork = unitOfWork;
        this._publisher = publisher;
    }

    /// <summary>
    /// <see cref="MediatR"/> implementation of the handler.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BookCartSeatsResponse> Handle(BookCartSeatsCommand request, CancellationToken cancellationToken)
    {
        var cart = await this._cartRepository.GetWithSeatsAsync(request.CartId, cancellationToken);

        if (cart is null)
            throw new NotFoundException($"Cart {request.CartId} is not found.");

        var payment = await this.SubmitOrder(cart, cancellationToken);

        await this._unitOfWork.SaveChanges(cancellationToken);
        await this.NotifyDependent(payment.Order, cancellationToken);

        return new BookCartSeatsResponse(payment.PaymentGuid);
    }

    private async Task<Payment> SubmitOrder(Cart cart, CancellationToken cancellation)
    {
        var order = Order.CreateFrom(cart);

        cart.BookSeats();
        await this._orderRepository.Add(order, cancellation);
        cart.Clear();

        return order.Payment;
    }

    private async Task NotifyDependent(Order order, CancellationToken cancellationToken)
    {
        await this._publisher.Publish(new SeatsBookedNotification(order), cancellationToken);
    }
}