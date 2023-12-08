namespace Ticketing.Domain.Entities.Event;

public class Price
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Amount { get; set; }
}
