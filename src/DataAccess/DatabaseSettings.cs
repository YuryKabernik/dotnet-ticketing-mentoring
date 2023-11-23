namespace Ticketing.DataAccess;

public class DatabaseSettings
{
    public required string ConnectionString { get; set; }
    public required TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(10)
}