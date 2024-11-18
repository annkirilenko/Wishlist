namespace Wishlist.Domain.Entities;

public class WishlistItemImage
{
    public Guid Id { get; set; }
    public Guid WishlistItemId { get; set; }
    public required string Filename { get; set; }
    public required string MimeType { get; set; }
}