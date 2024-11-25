namespace Wishlist.Domain.Exceptions;

public sealed class WishlistAlreadyExistsException : DomainException
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