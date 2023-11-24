using System.ComponentModel.DataAnnotations;

namespace Ticketing.DataAccess.Entities.Venue;

public class Row
{
    [Key]
    [Required]
    public required int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string Label { get; set; }
    
    [Required]
    public required int SectionId { get; set; }
    public virtual Section Section { get; set; } = null!;

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
