using Wishlist.Domain.Entities;
using Wishlist.Domain.Repositories;

namespace Wishlist.Application.Wishlist.UseCases.DeleteItem;

public class DeleteItemCommandHandler
{
    private readonly IWishlistItemRepository _wishlistItemRepository;

    public DeleteItemCommandHandler(IWishlistItemRepository wishlistItemRepository)
    {
        _wishlistItemRepository = wishlistItemRepository;
    }

    public async Task Handle(DeleteItemCommand command)
    {
        var wishlistItem = await _wishlistItemRepository.FindOneByItemIdAsync(command.ItemId);

        if (wishlistItem is null)
        {
            throw new InvalidOperationException("Wishlist item not found");
        }
        
        await _wishlistItemRepository.DeleteAsync(wishlistItem);
    }
}