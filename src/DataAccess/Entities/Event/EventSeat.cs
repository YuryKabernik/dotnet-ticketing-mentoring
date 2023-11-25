using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticketing.DataAccess.Entities.Event;

public class EventSeat
{
    [Key]
    [Required]
    public required int Id { get; set; }

    [Required]
    public required int RowId { get; set; }
    public virtual EventRow Row { get; set; } = null!;

    [Required]
    [Column(TypeName = "money")]
    public required decimal Price { get; set; }
}
