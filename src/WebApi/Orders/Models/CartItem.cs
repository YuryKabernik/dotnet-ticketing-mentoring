namespace Ticketing.WebApi.Orders.Models;

public record CartItem(int Id, string Status, decimal Price, int Row, int Section);
