using Ticketing.Application.Feature.Cart.Request;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Cart;

/// <summary>
/// Takes object of event_id, seat_id and price_id as a payload and adds a seat to the cart.
/// Returns a cart state (with total amount) back to the caller.
/// </summary>
public class CartUpdateCommand : ICommandHandler<CartUpdateWithSeatRequest>
{
    public readonly IUnitOfWork unitOfWork;
    private readonly ICartRepository cartRepository;
    private readonly IEventSeatRepository seatRepository;

    public CartUpdateCommand(
        ICartRepository cartRepository,
        IEventSeatRepository eventRepository,
        IUnitOfWork unitOfWork)
    {
        this.cartRepository = cartRepository;
        this.seatRepository = eventRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(CartUpdateWithSeatRequest request, CancellationToken cancellation)
    {
        var seat = await this.seatRepository.GetAsync(request.Payload.SeatId, cancellation);
        var cart = await this.cartRepository.GetAsync(request.CartId, cancellation);

        if (seat?.Row?.Section?.Event?.Id == request.Payload.EventId)
        {
            cart?.Seats.Add(seat);
        }

        await this.unitOfWork.SaveChanges(cancellation);
    }
}
