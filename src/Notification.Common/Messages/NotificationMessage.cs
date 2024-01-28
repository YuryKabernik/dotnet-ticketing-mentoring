namespace Ticketing.Notification.Common;

public record class NotificationMessage<TContent>
{
    public Guid TrackingId { get; init; } = Guid.NewGuid();

    /// <summary>
    /// "ticket added to checkout", "ticket successfully checked out"
    /// </summary>
    public string Operation { get; init; } = string.Empty;

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// (customer email, customer name)
    /// </summary>
    public Dictionary<string, string> Parameters { get; init; } = [];

    /// <summary>
    /// (info that you need in the notification – order amount, order summary)
    /// </summary>
    public TContent Content { get; init; } = default;
}
