using Ticketing.Domain.Entities.Ordering;
using Ticketing.Domain.Enums;

namespace Ticketing.Domain.Entities.Payments;

public class Payment
{
    public int Id { get; set; }
    public Guid PaymentGuid { get; set; }
    public decimal Price { get; set; }
    public PaymentStatusOption Status { get; set; }
    public virtual Order? Order { get; set; }
}
