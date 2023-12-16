using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.Application;

public record UpdateCartRequest(Guid CartId, UpdateCartPayload Payload);
public record UpdateCartPayload(int EventId, int SeatId, int PriceId);

public class CartUpdateCommand : ICommandHandler<UpdateCartRequest>
{
    private readonly ICartRepository cartRepository;
    private readonly IEventSeatRepository seatRepository;

    public CartUpdateCommand(ICartRepository cartRepository, IEventSeatRepository eventRepository)
    {
        this.cartRepository = cartRepository;
        this.seatRepository = eventRepository;
    }

    public async Task ExecuteAsync(UpdateCartRequest request, CancellationToken cancellation)
    {
        var seat = await this.seatRepository.GetAsync(request.Payload.SeatId, cancellation);

        if (seat?.Row?.Section?.Event?.Id == request.Payload.EventId)
        {
            var cart = await this.cartRepository.GetAsync(request.CartId, cancellation);
            cart?.Seats.Add(seat);
        }
    }
}
