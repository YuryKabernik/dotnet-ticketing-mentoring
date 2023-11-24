namespace Ticketing.DataAccess;

public class DatabaseSettings
{
    internal static readonly string SectionName = "TicketingDatabase";

    public required string ConnectionString { get; set; }
    public required int Timeout { get; set; }
    public required int RetryCount { get; set; }
    public required double RetryDelay { get; set; }
}