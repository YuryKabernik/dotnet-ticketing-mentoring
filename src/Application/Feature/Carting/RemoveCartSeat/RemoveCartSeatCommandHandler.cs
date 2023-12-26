using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application.Feature.Carting.RemoveCartSeat;

public class RemoveCartSeatCommandHandler : ICommandHandler<RemoveCartSeatCommand>
{
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveCartSeatCommandHandler(
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork)
    {
        this._cartRepository = cartRepository;
        this._unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(RemoveCartSeatCommand command, CancellationToken cancellation)
    {
        var cart = await this._cartRepository.GetWithSeatsEventsAsync(command.CartId, cancellation);
        NotFoundException.ThrowIfNull(cart);

        var seat = cart!.Seats.FirstOrDefault(seat =>
            seat.Row!.Section!.Event!.Id == command.EventId &&
            seat.Id == command.SeatId
        );
        NotFoundException.ThrowIfNull(seat);

        cart.Seats.Remove(seat!);

        await _unitOfWork.SaveChanges(cancellation);
    }
}
