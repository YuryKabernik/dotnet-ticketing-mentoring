using Riok.Mapperly.Abstractions;
using Ticketing.Application.Feature.Carting.UpdateCartSeats;
using Ticketing.WebApi.Models;

namespace Ticketing.WebApi;

[Mapper]
public static partial class SeatSelectionMapper
{
    public static partial SeatPayload ToSeatPayload(this SeatSelection seat);
}
