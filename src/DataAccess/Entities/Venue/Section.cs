using System.ComponentModel.DataAnnotations;

namespace Ticketing.DataAccess.Entities.Venue;

public class Section
{
    [Key]
    [Required]
    public required int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Label { get; set; }

    [Required]
    public required int VenueId { get; set; }
    public virtual Venue Venue { get; set; } = null!;
    
    public virtual ICollection<Row> Rows { get; set; } = new List<Row>();
}
