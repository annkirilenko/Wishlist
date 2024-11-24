namespace Wishlist.Domain.Exceptions;

public sealed class WishlistAlreadyExistsException : Exception
{
    public WishlistAlreadyExistsException()
    {
    }

    public WishlistAlreadyExistsException(string message) : base(message)
    {
    }

    public WishlistAlreadyExistsException(string message, Exception inner) : base(message, inner)
    {
    }
}