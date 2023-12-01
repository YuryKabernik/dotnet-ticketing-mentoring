using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Venue;

namespace Ticketing.DataAccess;

public class RowEntityTypeConfiguration : IEntityTypeConfiguration<Row>
{
    public void Configure(EntityTypeBuilder<Row> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Label)
            .HasMaxLength(50)
            .IsRequired();

        builder.Navigation(e => e.Section)
            .IsRequired();
    }
}
