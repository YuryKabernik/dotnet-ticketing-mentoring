using Ticketing.Domain.Interfaces;

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
        var seat = await this.seatRepository.FirstAsync(request.Payload.SeatId, cancellation);

        if (seat?.Row?.Section?.Event?.Id == request.Payload.EventId)
        {
            await this.cartRepository.AddSeat(request.CartId, seat, cancellation);
        }
    }
}
