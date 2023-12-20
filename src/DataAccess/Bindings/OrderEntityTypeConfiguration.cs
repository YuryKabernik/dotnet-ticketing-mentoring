using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Ordering;

namespace Ticketing.DataAccess.Bindings;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Navigation(p => p.User)
            .IsRequired();

        builder.Navigation(p => p.Status)
            .IsRequired();
    }
}
