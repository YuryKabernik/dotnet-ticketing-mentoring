using System.ComponentModel.DataAnnotations;
using Ticketing.DataAccess.Entities.Event;

namespace Ticketing.DataAccess.Entities;

public class Order
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;

    [Required]
    public int SeatId { get; set; }
    public virtual EventSeat Seat { get; set; } = null!;
}
