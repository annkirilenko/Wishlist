namespace Wishlist.Domain.ValueObjects;

public sealed class WishlistItemData
{
    public string Title { get; }
    public string? Description { get; }
    public string? Link { get; }
    public List<WishlistItemImageData> Images { get; }

    public WishlistItemData(
        string title, 
        string? description = null, 
        string? link = null,
        List<WishlistItemImageData>? images = null
        )
    {
        Title = title;
        Description = description;
        Link = link;
        Images = images ?? new List<WishlistItemImageData>();
    }
}