namespace Wishlist.Domain.Exceptions;

public class WishlistItemImageNumberExceededException : DomainException
{
    public WishlistItemImageNumberExceededException()
    {
    }

    public WishlistItemImageNumberExceededException(string message) : base(message)
    {
    }

    public WishlistItemImageNumberExceededException(string message, Exception inner) : base(message, inner)
    {
    }
}