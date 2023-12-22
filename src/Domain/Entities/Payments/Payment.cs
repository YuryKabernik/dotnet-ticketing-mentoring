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

    /// <summary>
    /// Updates payment status and moves all the seats related to a payment to the sold state.
    /// </summary>
    public void Complete()
    {
        this.Status = PaymentStatusOption.Completed;

        foreach (var seat in this.Order!.Seats!)
        {
            seat.Status = SeatStatusOption.Sold;
        }
    }

    /// <summary>
    /// Updates payment status and moves all the seats related to a payment to the available state.
    /// </summary>
    public void Fail()
    {
        this.Status = PaymentStatusOption.Failed;

        foreach (var seat in this.Order!.Seats!)
        {
            seat.Status = SeatStatusOption.Available;
        }
    }
}
