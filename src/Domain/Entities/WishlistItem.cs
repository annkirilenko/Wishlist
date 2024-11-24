using Wishlist.Domain.Enums;

namespace Wishlist.Domain.Entities;

public class WishlistItem
{
    public Guid Id { get; set; }
    public Guid WishlistId { get; set; }
    public WishlistItemStatus Status { get; set; }
    public bool Visible { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Link { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    
    public List<WishlistItemImage> Images { get; set; } = new List<WishlistItemImage>();

    public Reservation? Reservation { get; set; } = null;
}