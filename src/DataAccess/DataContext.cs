using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ticketing.DataAccess.Entities;
using Ticketing.DataAccess.Entities.Event;
using Ticketing.DataAccess.Entities.Venue;

namespace Ticketing.DataAccess;

public class DataContext : DbContext
{
    private readonly IOptions<DatabaseSettings> options;

    public DataContext(IOptions<DatabaseSettings> options)
    {
        this.options = options;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<Event> Events { get; set; }
}
