using Riok.Mapperly.Abstractions;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Entities.Event;

namespace Ticketing.Application;

[Mapper(AllowNullPropertyAssignment = true, UseReferenceHandling = true)]
public static partial class EventSeatMapper
{
    [MapperIgnoreSource((nameof(EventSeat.Cart)))]
    [MapProperty(nameof(EventSeat.Id), nameof(EventSeatDetails.SeatId))]
    [MapProperty(nameof(EventSeat.Row.Id), nameof(EventSeatDetails.RowId))]
    [MapProperty(nameof(EventSeat.Row.Section.Id), nameof(EventSeatDetails.SectionId))]
    [MapProperty(nameof(EventSeat.Order), nameof(EventSeatDetails.Status))]
    [MapProperty(nameof(EventSeat.Price), nameof(EventSeatDetails.Price))]
    public static partial EventSeatDetails ToDetailedResponse(EventSeat seat);

    [MapProperty(nameof(Price.Id), nameof(PriceOption.Id))]
    [MapProperty(nameof(Price.Name), nameof(PriceOption.Name))]
    public static partial PriceOption PriceToPriceOption(Price price);

    public static SeatStatus OrderToSeatStatus(Order order) => new()
    {
        Id = order.Status!.Id,
        Name = order.Status!.Name
    };
}
