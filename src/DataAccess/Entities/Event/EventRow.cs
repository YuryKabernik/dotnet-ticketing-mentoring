using System.ComponentModel.DataAnnotations;

namespace Ticketing.DataAccess.Entities.Event;

public class EventRow
{
    [Key]
    [Required]
    public required int Id { get; set; }

    [Required]
    public required int SectionId { get; set; }
    public virtual EventSection Section { get; set; } = null!;   

    public virtual ICollection<EventSeat> Seats { get; set; } = new List<EventSeat>();
}
