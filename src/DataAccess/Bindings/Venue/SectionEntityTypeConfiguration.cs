using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Venue;

namespace Ticketing.DataAccess;

public class SectionEntityTypeConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Label)
            .HasMaxLength(50)
            .IsRequired();

        builder.Navigation(p => p.Venue)
            .IsRequired();
    }
}
