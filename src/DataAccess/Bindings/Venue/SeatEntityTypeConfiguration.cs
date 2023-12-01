using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Venue;

namespace Ticketing.DataAccess;

public class SeatEntityTypeConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Label)
            .HasMaxLength(50)
            .IsRequired();

        builder.Navigation(p => p.Row)
            .IsRequired();
    }
}
