namespace Wishlist.Domain.Constants;

public static class ImageMimeTypes
{
    public static readonly HashSet<string> AllowedMimeTypes = new()
    {
        "image/jpeg",
        "image/png",
        "image/gif",
        "image/webp"
    };
    
    public static bool IsValid(string mimeType) => AllowedMimeTypes.Contains(mimeType);
}