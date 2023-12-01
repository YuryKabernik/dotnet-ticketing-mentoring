using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Entities.Venue;

namespace Ticketing.DataAccess;

public class DataContext : DbContext
{
    private readonly DatabaseSettings settings;

    public DataContext(DatabaseSettings settings)
    {
        this.settings = settings;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }

    #region Venue sets
    public DbSet<Venue> Venues { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Row> Rows { get; set; }
    public DbSet<Seat> Seats { get; set; }
    #endregion

    #region Event sets
    public DbSet<Event> Events { get; set; }
    public DbSet<EventSection> EventSections { get; set; }
    public DbSet<EventRow> EventRows { get; set; }
    public DbSet<EventSeat> EventSeats { get; set; }
    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        this.UseSqlServer(optionsBuilder);
    }

    private void UseSqlServer(DbContextOptionsBuilder optionsBuilder)
    {
        int timeoutSeconds = TimeSpan.FromSeconds(this.settings.Timeout).Seconds;
        TimeSpan retryDelaySeconds = TimeSpan.FromSeconds(this.settings.RetryDelay);

        optionsBuilder.UseSqlServer(
            this.settings.ConnectionString,
            providerOptions => providerOptions
                .CommandTimeout(timeoutSeconds)
                .EnableRetryOnFailure(this.settings.RetryCount, retryDelaySeconds, default)
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
