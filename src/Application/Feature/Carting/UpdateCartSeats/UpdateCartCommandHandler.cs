using MediatR;
using Ticketing.Application.CQRS;
using Ticketing.Application.Feature.Carting.UpdateCartSeats.Notifications;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Carting.UpdateCartSeats;

/// <summary>
/// Takes object of event_id, seat_id and price_id as a payload and adds a seat to the cart.
/// Returns a cart state (with total amount) back to the caller.
/// </summary>
public class UpdateCartCommandHandler : ICommandHandler<UpdateCartCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICartRepository _cartRepository;
    private readonly IEventSeatRepository _seatRepository;
    private readonly IPublisher _publisher;

    public UpdateCartCommandHandler(
        ICartRepository cartRepository,
        IEventSeatRepository eventRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        this._cartRepository = cartRepository;
        this._seatRepository = eventRepository;
        this._unitOfWork = unitOfWork;
        this._publisher = publisher;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        var seat = await this.GetSeat(request.Payload, cancellationToken);
        var cart = await this.GetCart(request.CartId, cancellationToken);

        cart.Add(seat);

        await this._unitOfWork.SaveChanges(cancellationToken);
        await this.NotifyDependent(seat, cancellationToken);
    }

    private async Task<Cart> GetCart(Guid cartId, CancellationToken cancellation)
    {
        var cart = await this._cartRepository.GetWithSeatsAsync(cartId, cancellation);

        if (cart is null)
            throw new NotFoundException($"Cart {cartId} was not found.");

        return cart;
    }

    private async Task<EventSeat> GetSeat(SeatPayload payload, CancellationToken cancellation)
    {
        var seat = await this._seatRepository.GetWithPriceEventAsync(payload.SeatId, cancellation);

        if (seat is null)
            throw new NotFoundException($"Seat {payload.SeatId} was not found.");

        var isExact = seat.IsExact(payload.SeatId, payload.EventId, payload.PriceId);

        if (!isExact)
        {
            throw new NotFoundException(
                $"Seat {payload.SeatId} of event {payload.EventId} and price {payload.PriceId} was not found."
            );
        }

        return seat;
    }

    private async Task NotifyDependent(EventSeat seat, CancellationToken cancellationToken)
    {
        await this._publisher.Publish(new SeatSelectedNotification(seat), cancellationToken);
    }
}
