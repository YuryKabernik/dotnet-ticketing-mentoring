using Ticketing.Domain.Entities.Ordering;
using Ticketing.Domain.Enums;
using Ticketing.Domain.Exceptions;

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
        if (this.Order is null)
            throw new NotFoundException($"Payment {PaymentGuid} doesn't have an associated Order.");

        if (this.Order.Seats.Count < 1)
            throw new NotFoundException($"Invalid number of seats in the Order {this.Order.Id}.");

        this.Status = PaymentStatusOption.Completed;
        this.Order.Status = OrderStatusOption.Paid;

        foreach (var seat in this.Order.Seats)
        {
            seat.Status = SeatStatusOption.Sold;
        }
    }

    /// <summary>
    /// Updates payment status and moves all the seats related to a payment to the available state.
    /// </summary>
    public void Fail()
    {
        if (this.Order is null)
            throw new NotFoundException($"Payment {PaymentGuid} doesn't have an associated Order.");

        if (this.Order.Seats.Count < 1)
            throw new NotFoundException($"Invalid number of seats in the Order {this.Order.Id}.");

        this.Status = PaymentStatusOption.Failed;
        this.Order.Status = OrderStatusOption.Canceled;

        foreach (var seat in this.Order.Seats)
        {
            seat.Status = SeatStatusOption.Available;
        }
    }
}
