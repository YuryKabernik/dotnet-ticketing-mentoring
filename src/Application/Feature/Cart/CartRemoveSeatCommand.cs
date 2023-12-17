using Ticketing.Application.Feature.Cart.Exception;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Cart;

public record CartRemoveSeatRequest(Guid CartId, int EventId, int SeatId);

public class CartRemoveSeatCommand : ICommandHandler<CartRemoveSeatRequest>
{
    public readonly ICartRepository cartRepository;
    public readonly IEventSeatRepository eventSeatRepository;
    public readonly IUnitOfWork unitOfWork;

    public CartRemoveSeatCommand(
        ICartRepository cartRepository,
        IEventSeatRepository eventSeatRepository,
        IUnitOfWork unitOfWork)
    {
        this.cartRepository = cartRepository;
        this.eventSeatRepository = eventSeatRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(CartRemoveSeatRequest request, CancellationToken cancellation)
    {
        var cart = await this.cartRepository.GetWithSeatsEventsAsync(request.CartId, cancellation);
        CartNotFoundExceptionException.ThrowIfNull(cart);

        var seat = cart!.Seats.FirstOrDefault(seat => seat.Row!.Section!.Event!.Id == request.EventId);
        SeatNotFoundExceptionException.ThrowIfNull(seat);

        cart.Seats.Remove(seat!);

        await unitOfWork.SaveChanges(cancellation);
    }
}

