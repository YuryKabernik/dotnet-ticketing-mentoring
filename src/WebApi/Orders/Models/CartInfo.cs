namespace Ticketing.WebApi.Models;

public record CartInfo(Guid CartId, IEnumerable<CartItem> Items, int Count);
