using MediatR;
using Ticketing.Domain.Entities.Ordering;

namespace Ticketing.Application.Feature.Carting.BookCartSeats.Notifications;

/// <summary>
/// Notification message over booked seats.
/// </summary>
internal class SeatsBookedNotification : INotification
{
    public SeatsBookedNotification(Order order)
    {
        this.Order = order;
    }

    public Order Order { get; }
}