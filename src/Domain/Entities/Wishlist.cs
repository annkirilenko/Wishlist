using Wishlist.Domain.ValueObjects;

namespace Wishlist.Domain.Entities;

public class Wishlist
{
    public Guid Id { get; }
    public Guid OwnerId { get; }
    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public List<WishlistItem> Items { get; set; }

    public Wishlist(Guid ownerId)
    {
        Id = Guid.NewGuid();
        OwnerId = ownerId;
        CreatedAt = DateTimeOffset.UtcNow;
        Items = new List<WishlistItem>();  
    }

    public void AddItem(WishlistItemData data)
    {
        
    }
}