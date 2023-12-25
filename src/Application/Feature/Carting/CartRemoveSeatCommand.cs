using Ticketing.Application.Feature.Cart.Request;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Carting;

public class CartRemoveSeatCommand : ICommandHandler<CartRemoveSeatRequest>
{
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CartRemoveSeatCommand(
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork)
    {
        this._cartRepository = cartRepository;
        this._unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(CartRemoveSeatRequest request, CancellationToken cancellation)
    {
        var cart = await this._cartRepository.GetWithSeatsEventsAsync(request.CartId, cancellation);
        NotFoundException.ThrowIfNull(cart);

        var seat = cart!.Seats.FirstOrDefault(seat =>
            seat.Row!.Section!.Event!.Id == request.EventId &&
            seat.Id == request.SeatId
        );
        NotFoundException.ThrowIfNull(seat);

        cart.Seats.Remove(seat!);

        await _unitOfWork.SaveChanges(cancellation);
    }
}
