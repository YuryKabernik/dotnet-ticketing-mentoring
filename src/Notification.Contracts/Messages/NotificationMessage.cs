namespace Ticketing.Notification.Contracts;

public record class NotificationMessage<TContent>
{
    public Guid TrackingId { get; } = Guid.NewGuid();

    /// <summary>
    /// "ticket added to checkout", "ticket successfully checked out"
    /// </summary>
    public string Operation { get; set; } = string.Empty;

    public DateTime Timestamp { get; } = DateTime.UtcNow;

    /// <summary>
    /// (customer email, customer name)
    /// </summary>
    public Dictionary<string, string> Parameters { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// (info that you need in the notification – order amount, order summary)
    /// </summary>
    public TContent Content { get; set; } = default;
}
