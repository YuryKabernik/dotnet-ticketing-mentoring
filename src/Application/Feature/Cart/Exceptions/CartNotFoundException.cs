namespace Ticketing.Application.Feature.Cart.Exception;

[Serializable]
public class CartNotFoundExceptionException : System.Exception
{
    public CartNotFoundExceptionException() { }
    public CartNotFoundExceptionException(string message) : base(message) { }
    public CartNotFoundExceptionException(string message, System.Exception inner) : base(message, inner) { }
    protected CartNotFoundExceptionException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    public static void ThrowIfNull(Domain.Entities.Cart? cart, string message = "Cart is not found")
    {
        if (cart is null)
        {
            throw new CartNotFoundExceptionException(message);
        }
    }
}