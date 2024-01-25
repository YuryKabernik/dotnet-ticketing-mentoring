using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Entities.Ordering;
using Ticketing.Domain.Entities.Payments;
using Ticketing.Domain.Entities.Venue;
using Ticketing.Domain.Exceptions;
using Ticketing.Domain.Interfaces;

namespace Ticketing.DataAccess;

public class DataContext : DbContext, IUnitOfWork
{
    private readonly DatabaseSettings settings;

    public DataContext(IOptions<DatabaseSettings> settings) : base()
    {
        this.settings = settings.Value;
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
            providerOptions => providerOptions.CommandTimeout(timeout.Seconds)
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }

    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        try
        {
            this.ChangeTracker.DetectChanges();
            await this.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException exception)
        {
            throw new ConflictOnChangeException("Save changes failed - concurrent changes detected!", exception);
        }
        catch (DbUpdateException exception)
        {
            throw new ConflictOnChangeException("Save changes failed - unable to apply the update!", exception);
        }
        catch (Exception exception) when (exception.InnerException is DbUpdateConcurrencyException or DbUpdateException)
        {
            throw new ConflictOnChangeException("Save changes failed!", exception);
        }
    }

    public async Task<IDbTransaction> BeginTransactionAsync(IsolationLevel level, CancellationToken cancellationTokens)
    {
        var transactionEf = await base.Database.BeginTransactionAsync(level, cancellationTokens);

        return transactionEf.GetDbTransaction();
    }
}
