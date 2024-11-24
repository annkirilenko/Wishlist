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

    public Wishlist(
        Guid id, 
        Guid ownerId, 
        DateTimeOffset createdAt, 
        DateTimeOffset? updatedAt,
        List<WishlistItem>? items = null
        ) : base()
    {
        Id = id;
        OwnerId = ownerId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Items = items ?? new List<WishlistItem>();
    }
}