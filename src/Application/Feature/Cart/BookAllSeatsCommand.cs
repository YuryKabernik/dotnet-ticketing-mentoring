using Ticketing.Application.Feature.Cart.Exception;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Entities.Ordering;
using Ticketing.Domain.Interfaces;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application;

public record BookAllSeatsRequest(Guid CartId);

/// <summary>
/// Moves all the seats in the cart to a booked state.
/// </summary>
public class BookAllSeatsCommand : ICommandHandler<BookAllSeatsRequest>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ICartRepository cartRepository;

    public BookAllSeatsCommand(ICartRepository cartRepository, IUnitOfWork unitOfWork)
    {
        this.cartRepository = cartRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(BookAllSeatsRequest request, CancellationToken cancellation)
    {
        var cart = await this.cartRepository.GetWithSeatsAsync(request.CartId, cancellation);
        CartNotFoundExceptionException.ThrowIfNull(cart);

        this.CreateOrderFromSeats(cart!.Seats);
        this.EmptyCart(cart);

        await this.unitOfWork.SaveChanges(cancellation);
    }

    private void CreateOrderFromSeats(IEnumerable<EventSeat> seats)
    {
        Order order = new() { Seats = seats.ToList() };
        
    }

    private void EmptyCart(Cart cart) => cart.Seats.Clear();
}
