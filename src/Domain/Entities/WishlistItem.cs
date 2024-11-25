using Wishlist.Domain.Enums;
using Wishlist.Domain.Exceptions;
using Wishlist.Domain.ValueObjects;

namespace Wishlist.Domain.Entities;

public class WishlistItem
{
    private const byte MaxImagesCount = 10;
    
    public Guid Id { get; }
    public Guid WishlistId { get; }
    public WishlistItemStatus Status { get; }
    public bool Visible { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Link { get; set; }
    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    
    public List<WishlistItemImage> Images { get; set; }

    public Reservation? Reservation { get; set; }

    public WishlistItem(
        Guid wishlistId, 
        string title, 
        string? description = null, 
        string? link = null,
        List<WishlistItemImage>? images = null
        )
    {
        Id = Guid.NewGuid();
        WishlistId = wishlistId;
        Status = WishlistItemStatus.Unreserved;
        Visible = true;
        Title = title;
        Description = description;
        Link = link;
        CreatedAt = DateTimeOffset.Now;
        Images = images ?? new List<WishlistItemImage>();
    }

    public void AddImage(WishlistItemImageData data)
    {
        if (Images.Count >= MaxImagesCount)
        {
            throw new WishlistItemImageNumberExceededException();
        }

        WishlistItemImage image = new WishlistItemImage(Id, data.Filename, data.MimeType);
        Images.Add(image);
    }
}