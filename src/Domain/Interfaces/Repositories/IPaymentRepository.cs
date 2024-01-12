using Ticketing.Domain.Entities.Payments;

namespace Ticketing.Domain.Interfaces.Repositories;

public interface IPaymentRepository
{
    Task<Payment?> GetAsync(Guid id, CancellationToken cancellation);
    Task<Payment?> GetWithSeatsAsync(Guid id, CancellationToken cancellation);
}
