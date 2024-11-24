namespace Wishlist.Domain.Repositories;

public interface IWishlistRepository
{
    public Task<Entities.Wishlist?> FindByOwnerIdAsync(Guid ownerId);
    public Task CreateAsync(Entities.Wishlist wishlist);
    public Task<Entities.Wishlist> GetByOwnerIdAsync(Guid ownerId);
}