using Microsoft.EntityFrameworkCore;
using Ticketing.Domain;
using Ticketing.Domain.Entities.Payments;

namespace Ticketing.DataAccess;

public class PaymentRepository : IPaymentRepository
{
    private readonly DataContext context;

    public PaymentRepository(DataContext context)
    {
        this.context = context;
    }

    public Task<Payment?> GetAsync(Guid id, CancellationToken cancellation)
    {
        return this.context.Payments
            .SingleOrDefaultAsync(payment => payment.PaymentGuid == id, cancellation);
    }

    public Task<Payment?> GetWithSeatsAsync(Guid id, CancellationToken cancellation)
    {
        return this.context.Payments
            .Include(payment => payment.Order)
            .ThenInclude(order => order!.Seats)
            .SingleOrDefaultAsync(payment => payment.PaymentGuid == id, cancellation);
    }
}
