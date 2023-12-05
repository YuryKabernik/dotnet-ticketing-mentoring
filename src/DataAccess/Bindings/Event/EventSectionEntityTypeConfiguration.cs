using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Event;

namespace Ticketing.DataAccess;

public class EventSectionEntityTypeConfiguration : IEntityTypeConfiguration<EventSection>
{
    public void Configure(EntityTypeBuilder<EventSection> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Navigation(p => p.Event)
            .IsRequired();
    }
}
