using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticketing.DataAccess.Entities.Event;

public class Event
{
    [Key]
    [Required]
    public required int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    public required int VenueId { get; set; }
    public virtual Venue.Venue Venue { get; set; } = null!;

    [Required]
    [Column(TypeName = "datetime")]
    public required DateTime Date { get; set; }
}
