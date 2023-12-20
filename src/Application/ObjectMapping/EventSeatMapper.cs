using Riok.Mapperly.Abstractions;
using Ticketing.Domain.Entities.Ordering;
using Ticketing.Domain.Entities.Event;

namespace Ticketing.Application.ObjectMapping;

[Mapper(AllowNullPropertyAssignment = true, UseReferenceHandling = true)]
public static partial class EventSeatMapper
{
    [MapperIgnoreSource((nameof(EventSeat.Cart)))]
    [MapProperty(nameof(EventSeat.Id), nameof(EventSeatDetails.SeatId))]
    [MapProperty(nameof(@EventSeat.Row.Id), nameof(EventSeatDetails.RowId))]
    [MapProperty(nameof(@EventSeat.Row.Section.Id), nameof(EventSeatDetails.SectionId))]
    [MapProperty(nameof(EventSeat.Price), nameof(EventSeatDetails.Price))]
    [MapProperty(nameof(EventSeat.Order), nameof(EventSeatDetails.Status))]
    public static partial EventSeatDetails ToDetailedResponse(EventSeat seat);
    
    [MapProperty(nameof(Price.Id), nameof(PriceOption.Id))]
    [MapProperty(nameof(Price.Name), nameof(PriceOption.Name))]
    public static partial PriceOption ToPriceOption(Price price);
    
    [MapProperty(nameof(@Order.Status.Id), nameof(SeatStatus.Id))]
    [MapProperty(nameof(@Order.Status.Name), nameof(SeatStatus.Name))]
    public static partial SeatStatus ToSeatStatus(Order order);
}
