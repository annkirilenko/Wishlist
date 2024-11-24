using Wishlist.Application.Common.Interfaces;
using Wishlist.Domain.Exceptions;
using Wishlist.Domain.Repositories;

namespace Wishlist.Application.Wishlist.UseCases.CreateWishlist;

public sealed class CreateWishlistCommandHandler
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IUser _currentUser;

    public CreateWishlistCommandHandler(IUser currentUser, IWishlistRepository wishlistRepository)
    {
        _currentUser = currentUser;
        _wishlistRepository = wishlistRepository;
    }

    public async Task Handle(CreateWishlistCommand command)
    {
        Domain.Entities.Wishlist? existingWishlist = await _wishlistRepository.FindByOwnerIdAsync(_currentUser.Id);

        if (existingWishlist is not null)
        {
            throw new WishlistAlreadyExistsException();
        }

        Domain.Entities.Wishlist wishlist = new Domain.Entities.Wishlist(_currentUser.Id);
        await _wishlistRepository.CreateAsync(wishlist);
    }
}