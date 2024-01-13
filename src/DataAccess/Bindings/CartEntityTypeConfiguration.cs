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

        builder.HasOne(p => p.User)
            .WithOne(p => p.Cart)
            .HasForeignKey<Cart>(c => c.Id)
            .IsRequired(false);

        builder.Navigation(p => p.Seats);

        builder.Ignore(p => p.FinalPrice);
    }
}
