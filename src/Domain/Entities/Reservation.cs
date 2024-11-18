namespace Wishlist.Domain.Entities;

public class Reservation
{
    public Guid Id { get; set; }
    public Guid WishlistGuid { get; set; }
    public Guid WishlistItemId { get; set; }
    public Guid ReserverId { get; set; }
    public DateTimeOffset? ConfirmTill { get; set; }
    public DateTimeOffset? CancelledAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}