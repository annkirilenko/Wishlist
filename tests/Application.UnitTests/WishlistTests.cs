using Moq;
using Wishlist.Application.Common.Interfaces;
using Wishlist.Application.Wishlist.UseCases.CreateWishlist;
using Wishlist.Domain.Exceptions;
using Wishlist.Domain.Repositories;

namespace Wishlist.Application.UnitTests;

public sealed class WishlistTests
{
    [Fact]
    public async Task CreateWishlist_Should_Create_Wishlist()
    {
        // Arrange 
        Guid currentUserId = Guid.NewGuid();
        
        Mock<IUser> currentUser = new Mock<IUser>();
        currentUser.Setup(u => u.Id).Returns(currentUserId);
        
        Mock<IWishlistRepository> wishlistRepository = new Mock<IWishlistRepository>();
        wishlistRepository.Setup(r => r.FindByOwnerIdAsync(currentUserId)).ReturnsAsync(null as Domain.Entities.Wishlist);
        
        // Act 
        CreateWishlistCommandHandler sut = new CreateWishlistCommandHandler(currentUser.Object, wishlistRepository.Object);
        CreateWishlistCommand command = new CreateWishlistCommand();
        await sut.Handle(command);
        
        // Assert
        wishlistRepository.Verify(
                r => r.CreateAsync(It.Is<Domain.Entities.Wishlist>(w => w.OwnerId == currentUserId)), 
                Times.Once);
    }
    
    [Fact]
    public async Task CreateWishlist_Should_Throw_WishlistAlreadyExists_When_Create_Another_Wishlist()
    {
        // Arrange 
        Guid currentUserId = Guid.NewGuid();
        Domain.Entities.Wishlist existingWishlist = new Domain.Entities.Wishlist(currentUserId);
        
        Mock<IUser> currentUser = new Mock<IUser>();
        currentUser.Setup(u => u.Id).Returns(currentUserId);
        
        Mock<IWishlistRepository> wishlistRepository = new Mock<IWishlistRepository>();
        wishlistRepository.Setup(r => r.FindByOwnerIdAsync(currentUserId)).ReturnsAsync(existingWishlist);
        
        // Act 
        CreateWishlistCommandHandler sut = new CreateWishlistCommandHandler(currentUser.Object, wishlistRepository.Object);
        CreateWishlistCommand command = new CreateWishlistCommand();
        
        // Assert
        await Assert.ThrowsAsync<WishlistAlreadyExistsException>(() => sut.Handle(command));
    }
}