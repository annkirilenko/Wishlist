using Wishlist.Domain.Constants;

namespace Wishlist.Domain.Entities;

public class WishlistItemImage
{
    public Guid Id { get; }
    public Guid WishlistItemId { get; }
    public string Filename { get; }
    public string MimeType { get; }

    public WishlistItemImage(Guid wishlistItemId, string filename, string mimeType)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            throw new ArgumentException($"'{nameof(filename)}' cannot be null or whitespace.", nameof(filename));
        }

        if (string.IsNullOrWhiteSpace(mimeType) || !ImageMimeTypes.IsValid(mimeType.Trim()))
        {
            throw new ArgumentException($"'{nameof(mimeType)}' is not a valid mime type.", nameof(mimeType));
        }
        
        Id = Guid.NewGuid();
        WishlistItemId = wishlistItemId;
        Filename = filename.Trim();
        MimeType = mimeType.Trim();
    }
}