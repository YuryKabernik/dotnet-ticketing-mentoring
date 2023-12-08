using Ticketing.Domain.Entities.Event;

namespace Ticketing.Domain;

public class Price
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Amount { get; set; }
}
