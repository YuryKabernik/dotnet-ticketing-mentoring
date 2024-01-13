using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Venue;

namespace Ticketing.DataAccess.Bindings;

public class VenueEntityTypeConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Navigation(e => e.Address)
            .IsRequired();
        
        builder.Navigation(e => e.Sections);
    }
}
