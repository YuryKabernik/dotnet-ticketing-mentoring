using Riok.Mapperly.Abstractions;
using Ticketing.Application.Feature.Event.Response;
using Ticketing.Domain.Entities.Event;
using Ticketing.Domain.Enums;

namespace Ticketing.Application.ObjectMapping;

[Mapper(AllowNullPropertyAssignment = true, UseReferenceHandling = true)]
public static partial class EventSeatMapper
{
    [MapperIgnoreSource((nameof(EventSeat.Cart)))]
    [MapperIgnoreSource((nameof(EventSeat.Order)))]
    [MapProperty(nameof(EventSeat.Id), nameof(EventSeatDetails.SeatId))]
    [MapProperty(nameof(@EventSeat.Row.Id), nameof(EventSeatDetails.RowId))]
    [MapProperty(nameof(@EventSeat.Row.Section.Id), nameof(EventSeatDetails.SectionId))]
    [MapProperty(nameof(EventSeat.Price), nameof(EventSeatDetails.Price))]
    [MapProperty(nameof(EventSeat.Status), nameof(EventSeatDetails.Status))]
    public static partial EventSeatDetails ToDetailedResponse(EventSeat seat);

    [MapProperty(nameof(Price.Id), nameof(PriceOption.Id))]
    [MapProperty(nameof(Price.Name), nameof(PriceOption.Name))]
    public static partial PriceOption ToPriceOption(Price price);

    public static SeatStatus ToSeatStatus(SeatStatusOption option) => new
    (
        (int)option,
        Enum.GetName(option) ?? Enum.GetName(SeatStatusOption.Available)!
    );
}
