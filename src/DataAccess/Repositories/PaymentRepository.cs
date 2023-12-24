using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities.Payments;
using Ticketing.Domain.Interfaces.Repositories;

namespace Ticketing.DataAccess.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly DataContext _context;

    public PaymentRepository(DataContext context)
    {
        this._context = context;
    }

    public Task<Payment> GetAsync(Guid id, CancellationToken cancellation)
    {
        return this._context.Payments
            .SingleAsync(payment => payment.PaymentGuid == id, cancellation);
    }

    public Task<Payment> GetWithSeatsAsync(Guid id, CancellationToken cancellation)
    {
        return this._context.Payments
            .Include(payment => payment.Order)
            .ThenInclude(order => order!.Seats)
            .SingleAsync(payment => payment.PaymentGuid == id, cancellation);
    }
}
