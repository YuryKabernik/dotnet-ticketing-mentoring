using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Event;

namespace Ticketing.DataAccess;

public class EventRowEntityTypeConfiguration : IEntityTypeConfiguration<EventRow>
{
    public void Configure(EntityTypeBuilder<EventRow> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Navigation(p => p.Section)
            .IsRequired();
    }
}
