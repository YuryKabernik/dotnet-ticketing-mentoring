namespace Ticketing.DataAccess;

public class DatabaseSettings
{
    public required string ConnectionString { get; set; }
    public required int TimeoutSeconds { get; set; }
    public required int RetryAttempts { get; set; }
    public required int RetryDelaySeconds { get; set; }
}