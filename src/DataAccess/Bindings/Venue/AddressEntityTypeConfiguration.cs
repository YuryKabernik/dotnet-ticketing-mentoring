using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Venue;

namespace Ticketing.DataAccess.Bindings;

public class AddressEntityTypeConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Country)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.City)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Street)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Building)
            .HasMaxLength(200)
            .IsRequired();
    }
}
