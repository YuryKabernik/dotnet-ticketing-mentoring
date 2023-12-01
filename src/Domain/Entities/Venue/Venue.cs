namespace Ticketing.Domain.Entities.Venue;

public class Venue
{
    public required int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required Address Address { get; set; } = null!;
}

public class Address
{
    public required int Id { get; set; }

    public required string Country { get; set; }

    public required string City { get; set; }

    public required string Street { get; set; }

    public required string Building { get; set; }
}
