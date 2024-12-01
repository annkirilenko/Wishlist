using Wishlist.Domain.Entities;

namespace Wishlist.Domain.Repositories;

public interface IWishlistItemRepository
{
    public Task<WishlistItem?> FindOneByItemIdAsync(Guid itemId);

    public Task DeleteAsync(WishlistItem wishlistItem);
}