using Ticketing.Domain.Entities.Payments;

namespace Ticketing.Domain.Interfaces.Repositories;

public interface IPaymentRepository
{
    public Task<Payment> GetAsync(Guid id, CancellationToken cancellation);
    public Task<Payment> GetWithSeatsAsync(Guid id, CancellationToken cancellation);
}
