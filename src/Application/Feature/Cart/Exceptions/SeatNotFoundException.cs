using Ticketing.Domain.Entities.Event;

namespace Ticketing.Application;

[System.Serializable]
public class SeatNotFoundExceptionException : System.Exception
{
    public SeatNotFoundExceptionException() { }
    public SeatNotFoundExceptionException(string message) : base(message) { }
    public SeatNotFoundExceptionException(string message, System.Exception inner) : base(message, inner) { }
    protected SeatNotFoundExceptionException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    public static void ThrowIfNull(EventSeat? eventSeat, string message = "Seat is not found")
    {
        if (eventSeat is null)
        {
            throw new SeatNotFoundExceptionException(message);
        }
    }
}