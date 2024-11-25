namespace Wishlist.Application.Wishlist.UseCases.AddItem;

public sealed class AddItemCommand
{
    public Guid WishlistId { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public string? Link { get; init; }
    public List<ItemImage>? Images { get; init; }
    
    public AddItemCommand(Guid wishlistId, string title, string? description, string? link, List<ItemImage>? images = null)
    {
        WishlistId = wishlistId;
        Title = title.Trim();
        Description = description?.Trim();
        Link = link?.Trim();
        Images = images ?? new List<ItemImage>();
    }
}

public record ItemImage(string Filename, string MimeType);