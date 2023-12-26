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

    public UpdateCartCommandHandler(
        ICartRepository cartRepository,
        IEventSeatRepository eventRepository,
        IUnitOfWork unitOfWork)
    {
        this._cartRepository = cartRepository;
        this._seatRepository = eventRepository;
        this._unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(UpdateCartCommand request, CancellationToken cancellation)
    {
        var seat = await GetSeat(request.Payload, cancellation);
        var cart = await GetCart(request.CartId, cancellation);

        if (seat.IsExact(
                request.Payload.SeatId,
                request.Payload.EventId,
                request.Payload.PriceId)
           )
        {
            cart?.Seats.Add(seat);
        }

        await this._unitOfWork.SaveChanges(cancellation);
    }

    private async Task<Domain.Entities.Cart> GetCart(Guid cartId, CancellationToken cancellation)
    {
        var cart = await this._cartRepository.GetWithSeatsAsync(cartId, cancellation);
        NotFoundException.ThrowIfNull(cart);

        return cart!;
    }

    private async Task<EventSeat> GetSeat(SeatPayload payload, CancellationToken cancellation)
    {
        var seat = await this._seatRepository.GetWithPriceEventAsync(payload.SeatId, cancellation);
        NotFoundException.ThrowIfNull(seat);

        return seat;
    }
}