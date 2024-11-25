using Wishlist.Domain.Enums;
using Wishlist.Domain.Exceptions;
using Wishlist.Domain.ValueObjects;

namespace Wishlist.Domain.Entities;

public class WishlistItem
{
    private const byte MaxImagesCount = 10;
    
    public Guid Id { get; }
    public Guid WishlistId { get; }
    public WishlistItemStatus Status { get; private set; }
    public bool Visible { get; private set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Link { get; set; }
    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    
    public List<WishlistItemImage> Images { get; private set; }

    public Reservation? Reservation { get; set; }

    public WishlistItem(
        Guid wishlistId, 
        string title, 
        string? description = null, 
        string? link = null
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
        Images = new List<WishlistItemImage>();
    }

    public void AddImage(WishlistItemImageData data)
    {
        if (Images.Count >= MaxImagesCount)
        {
            throw new WishlistItemImageNumberExceededException();
        }

        WishlistItemImage image = new WishlistItemImage(Id, data.Filename, data.MimeType);
        Images.Add(image);
        
        UpdatedAt = DateTimeOffset.Now;
    }
}