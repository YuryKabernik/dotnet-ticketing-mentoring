using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Ordering;
using Ticketing.Domain.Enums;

namespace Ticketing.DataAccess.Bindings;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Status)
            .HasDefaultValue(OrderStatusOption.Submitted)
            .IsRequired();

        builder.Navigation(p => p.User)
            .IsRequired();

        builder.Navigation(p => p.Seats)
            .IsRequired();
    }
}
