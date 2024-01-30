namespace Ticketing.DataAccess.Setup;

public class DatabaseSettings
{
    public static readonly string SectionName = "Database";
    public required string ConnectionString { get; init; }
    public required int TimeoutSeconds { get; init; }
    public required int RetryAttempts { get; init; }
    public required int RetryDelaySeconds { get; init; }
}