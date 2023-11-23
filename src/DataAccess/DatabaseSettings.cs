namespace Ticketing.DataAccess;

public class DatabaseSettings
{
    internal static readonly string SectionName = "TicketingDatabase";

    public required string ConnectionString { get; set; }
}