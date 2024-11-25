namespace Wishlist.Domain.ValueObjects;

public class WishlistItemImageData
{
    public string Filename { get; }
    public string MimeType { get; }

    public WishlistItemImageData(string filename, string mimeType)
    {
        Filename = filename.Trim();
        MimeType = mimeType.Trim().ToLower();
    }
}