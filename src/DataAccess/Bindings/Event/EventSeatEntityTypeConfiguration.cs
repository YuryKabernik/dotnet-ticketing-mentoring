using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Event;

namespace Ticketing.DataAccess;

public class EventSeatEntityTypeConfiguration : IEntityTypeConfiguration<EventSeat>
{
    public void Configure(EntityTypeBuilder<EventSeat> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Navigation(p => p.Row)
            .IsRequired();
        
        builder.Navigation(p => p.Price)
            .IsRequired();

        builder.Navigation(p => p.Order)
            .IsRequired(false);

        builder.Navigation(p => p.Cart)
            .IsRequired(false);
    }
}
