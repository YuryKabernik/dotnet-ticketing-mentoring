namespace Ticketing.DataAccess;

public class DatabaseSettings
{
    internal static string SectionName = "TicketingDatabase";

    public required string ConnectionString { get; set; }
    public required TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(10);
}