using Wishlist.Application.Common.Exceptions;
using Wishlist.Application.Common.Interfaces;
using Wishlist.Domain.Repositories;
using Wishlist.Domain.ValueObjects;

namespace Wishlist.Application.Wishlist.UseCases.AddItem;

public class AddItemCommandHandler
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IUser _currentUser;
    
    public AddItemCommandHandler(IUser currentUser, IWishlistRepository wishlistRepository)
    {
        _currentUser = currentUser;
        _wishlistRepository = wishlistRepository;
    }
    
    public async Task Handle(AddItemCommand command)
    {
        Domain.Entities.Wishlist wishlist = await _wishlistRepository.GetByIdAsync(command.WishlistId);

        if (wishlist.OwnerId != _currentUser.Id)
        {
            throw new UnauthorizedWishlistAccessException();
        }

        wishlist.AddItem(new WishlistItemData(
            title: command.Title,
            description: command.Description,
            link: command.Link,
            images: command.Images?
                        .Select(i => new WishlistItemImageData(i.Filename, i.MimeType))
                        .ToList()
        ));
        
        await _wishlistRepository.UpdateAsync(wishlist);
    }
}