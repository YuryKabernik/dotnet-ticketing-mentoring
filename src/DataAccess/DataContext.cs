using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Entities.Ordering;
using Ticketing.Domain.Entities.Payments;
using Ticketing.Domain.Entities.Venue;
using Ticketing.Domain.Interfaces;

namespace Ticketing.DataAccess;

public class DataContext : DbContext, IUnitOfWork
{
    private readonly DatabaseSettings settings;

    public DataContext(DatabaseSettings settings)
    {
        this.settings = settings;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Cart> Carts { get; set; }

    public DbSet<Payment> Payments { get; set; }

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

        optionsBuilder.UseLazyLoadingProxies();
    }

    private void UseSqlServer(DbContextOptionsBuilder optionsBuilder)
    {
        TimeSpan timeout = TimeSpan.FromSeconds(this.settings.TimeoutSeconds);
        TimeSpan delay = TimeSpan.FromSeconds(this.settings.RetryDelaySeconds);

        optionsBuilder.UseSqlServer(
            this.settings.ConnectionString,
            providerOptions => providerOptions
                .CommandTimeout(timeout.Seconds)
                .EnableRetryOnFailure(this.settings.RetryAttempts, delay, default)
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }

    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        await this.SaveChangesAsync(cancellationToken);
    }
}
