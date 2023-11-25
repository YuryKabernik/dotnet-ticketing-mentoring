using System.ComponentModel.DataAnnotations;

namespace Ticketing.DataAccess.Entities.Event;

public class EventSection
{
    [Key]
    [Required]
    public required int Id { get; set; }

    [Required]
    public required int EventId { get; set; }
    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<EventRow> Rows { get; set; } = new List<EventRow>();
}
