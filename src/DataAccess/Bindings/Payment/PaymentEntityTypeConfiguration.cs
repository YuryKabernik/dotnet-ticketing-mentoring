using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities.Payments;
using Ticketing.Domain.Enums;

namespace Ticketing.DataAccess.Bindings;

public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.PaymentGuid)
            .HasDefaultValueSql("NEWSEQUENTIALID()")
            .IsRequired();

        builder.Property(p=> p.Price)
            .HasColumnType("money")
            .IsRequired();

        builder.Property(p => p.Status)
            .HasDefaultValue(PaymentStatusOption.Pending)
            .IsRequired();

        builder.Navigation(p => p.Order)
            .IsRequired();
    }
}
