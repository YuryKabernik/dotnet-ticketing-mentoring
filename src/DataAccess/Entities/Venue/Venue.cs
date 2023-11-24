using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticketing.DataAccess.Entities.Venue;

public class Venue
{
    [Key]
    [Required]
    public required int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    public required Address Address { get; set; }

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
}

[Table("Addresses")]
public class Address
{
    [Key]
    [Required]
    public required int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Country { get; set; }

    [Required]
    [MaxLength(200)]
    public required string City { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Street { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Building { get; set; }
}