using Wishlist.Domain.ValueObjects;

namespace Wishlist.Domain.Entities;

public sealed class Wishlist
{
    public Guid Id { get; }
    public Guid OwnerId { get; }
    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public List<WishlistItem> Items { get; private set; }

    public Wishlist(Guid ownerId)
    {
        Id = Guid.NewGuid();
        OwnerId = ownerId;
        CreatedAt = DateTimeOffset.UtcNow;
        Items = new List<WishlistItem>();  
    }

    public void AddItem(WishlistItemData data)
    {
        WishlistItem item = new WishlistItem(Id, data.Title, data.Description, data.Link);

        foreach (var image in data.Images)
        {
            item.AddImage(image);
        }
        
        Items.Add(item);
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}