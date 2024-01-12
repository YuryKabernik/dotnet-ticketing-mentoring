using Ticketing.Application.CQRS;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Event;
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task Handle(RemoveCartSeatCommand request, CancellationToken cancellationToken) =>
        this.ExecuteAsync(request, cancellationToken);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task ExecuteAsync(RemoveCartSeatCommand command, CancellationToken cancellation)
    {
        var cart = await GetCartAsync(command, cancellation);
        var seat = GetSeat(command, cart);

        cart.Seats.Remove(seat);

        await this._unitOfWork.SaveChanges(cancellation);
    }

    private async Task<Cart> GetCartAsync(RemoveCartSeatCommand command, CancellationToken cancellation)
    {
        var cart = await this._cartRepository.GetWithSeatsEventsAsync(command.CartId, cancellation);

        if (cart is null)
            throw new NotFoundException($"Cart {command.CartId} is not found.");

        return cart;
    }
    
    private static EventSeat GetSeat(RemoveCartSeatCommand command, Cart cart)
    {
        var seat = cart.Seats.FirstOrDefault(seat => seat.Id == command.SeatId);

        if (seat is null)
            throw new NotFoundException($"Seat {command.SeatId} is not found.");

        return seat;
    }
}
