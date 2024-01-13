namespace Ticketing.WebApi.Orders.Models;

public record CartInfo(Guid CartId, IEnumerable<CartItem> Items, int Count);
