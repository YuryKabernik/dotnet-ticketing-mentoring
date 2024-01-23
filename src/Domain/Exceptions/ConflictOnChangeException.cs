namespace Ticketing.Domain.Exceptions;

[Serializable]
public class ConflictOnChangeException : Exception
{
    public ConflictOnChangeException() { }
    public ConflictOnChangeException(string message) : base(message) { }
    public ConflictOnChangeException(string message, Exception inner) : base(message, inner) { }
    protected ConflictOnChangeException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
