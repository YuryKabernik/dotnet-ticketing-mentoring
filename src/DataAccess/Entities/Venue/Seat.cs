using System.ComponentModel.DataAnnotations;

namespace Ticketing.DataAccess.Entities.Venue;

public class Seat
{
    [Key]
    [Required]
    public required int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Label { get; set; }

    [Required]
    public required int RowId { get; set; }
    public virtual Row Row { get; set; } = null!;
}
