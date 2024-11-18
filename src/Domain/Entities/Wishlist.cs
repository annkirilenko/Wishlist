namespace Wishlist.Domain.Entities;

public class Wishlist
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}