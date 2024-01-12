﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Enums;

namespace Ticketing.DataAccess;

public class EventSeatEntityTypeConfiguration : IEntityTypeConfiguration<EventSeat>
{
    public void Configure(EntityTypeBuilder<EventSeat> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Status)
            .HasDefaultValue(SeatStatusOption.Available)
            .IsRequired();

        builder.Navigation(p => p.Row)
            .IsRequired();

        builder.Navigation(p => p.Price)
            .IsRequired();

        builder.Navigation(p => p.Order)
            .IsRequired(false);

        builder.Navigation(p => p.Cart)
            .IsRequired(false);

        builder.Navigation(p => p.Payment)
            .IsRequired(false);
    }
}
