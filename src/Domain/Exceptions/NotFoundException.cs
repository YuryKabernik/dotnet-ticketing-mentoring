using System.Runtime.Serialization;

namespace Ticketing.Domain.Exceptions;

[Serializable]
public class NotFoundException : Exception
{
    public NotFoundException() { }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception inner) : base(message, inner) { }
    protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public static void ThrowIfNull(object? obj, string? message = null)
    {
        if (obj is not null)
            return;

        string msg = message ?? $"{obj!.GetType().Name} is not found.";

        throw new NotFoundException(msg);
    }
}
