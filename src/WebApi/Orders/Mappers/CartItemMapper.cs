using Riok.Mapperly.Abstractions;
using Ticketing.Application.Feature.Carting.QueryCart;
using Ticketing.Domain.Entities.Event;
using Ticketing.WebApi.Orders.Models;

namespace Ticketing.WebApi.Orders.Mappers;

[Mapper]
public static partial class CartItemMapper
{
    [MapProperty(nameof(CartQueryResponse.CartId), nameof(CartInfo.CartId))]
    [MapProperty(nameof(CartQueryResponse.Seats), nameof(CartInfo.Items))]
    [MapProperty(nameof(@CartQueryResponse.Seats.Count), nameof(CartInfo.Count))]
    public static partial CartInfo ToCartInfo(this CartQueryResponse response);

    [MapProperty(nameof(EventSeat.Id), nameof(CartItem.Id))]
    [MapProperty(nameof(EventSeat.Status), nameof(CartItem.Status))]
    [MapProperty(nameof(@EventSeat.Price.Amount), nameof(CartItem.Price))]
    [MapProperty(nameof(@EventSeat.Row.Id), nameof(CartItem.Row))]
    [MapProperty(nameof(@EventSeat.Row.Section.Id), nameof(CartItem.Section))]
    private static partial CartItem ToCartItem(this EventSeat seat);
}
