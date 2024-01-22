using MediatR;
using Ticketing.Domain.Entities.Event;

namespace Ticketing.Application.Feature.Carting.UpdateCartSeats.Notifications;

/// <summary>
/// Notification message over the selected event seat.
/// </summary>
internal class SeatSelectedNotification : INotification
{
    public SeatSelectedNotification(EventSeat seat)
    {
        this.Seat = seat;
    }

    public EventSeat Seat { get; init; }
}
