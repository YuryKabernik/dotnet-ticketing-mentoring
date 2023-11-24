using System.ComponentModel.DataAnnotations;

namespace Ticketing.DataAccess.Entities;

public class User
{
    [Key]
    [Required]
    public required int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Surname { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Email { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = null!;
}
