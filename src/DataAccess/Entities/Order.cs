namespace Ticketing.DataAccess.Entities;

public class Order
{
    public required int Id { get; set; }
    public required int UserId { get; set; }
    public required int SeatId { get; set; }
}
