using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities;

namespace Ticketing.DataAccess.Bindings;

public class CartEntityTypeConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Guid)
            .IsRequired();

        builder.Property(p => p.CreatedOn)
            .HasDefaultValueSql("getdate()")
            .IsRequired();

        builder.Navigation(p => p.User).IsRequired(false);
        builder.Navigation(p => p.Seats).IsRequired(false);
    }
}
