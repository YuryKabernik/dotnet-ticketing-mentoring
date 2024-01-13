using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities;

namespace Ticketing.DataAccess.Bindings;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Surname)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Phone)
            .HasMaxLength(50)
            .IsRequired();

        builder.Navigation(p => p.Cart).IsRequired(false);
        builder.Navigation(p => p.Orders);
    }
}
