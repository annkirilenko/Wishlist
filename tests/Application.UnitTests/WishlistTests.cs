using Moq;
using Wishlist.Application.Common.Exceptions;
using Wishlist.Application.Common.Interfaces;
using Wishlist.Application.Wishlist.UseCases.AddItem;
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

    [Fact]
    public async Task AddItem_Should_Add_WishlistItem()
    {
        // Arrange
        Guid currentUserId = Guid.NewGuid();
        Domain.Entities.Wishlist wishlist = new Domain.Entities.Wishlist(currentUserId);
        DateTimeOffset? wishlistUpdatedAt = wishlist.UpdatedAt;
        
        Mock<IUser> currentUser = new Mock<IUser>();
        currentUser.Setup(u => u.Id).Returns(currentUserId);
        
        Mock<IWishlistRepository> wishlistRepository = new Mock<IWishlistRepository>();
        wishlistRepository.Setup(r => r.GetByIdAsync(wishlist.Id)).ReturnsAsync(wishlist);
        wishlistRepository.Setup(r => r.UpdateAsync(wishlist)).Returns(Task.CompletedTask);
        
        // Act
        AddItemCommandHandler sut = new AddItemCommandHandler(currentUser.Object, wishlistRepository.Object);
        AddItemCommand command = new AddItemCommand(
            wishlist.Id, 
            "Item 1", 
            "Item description", 
            "https://www.google.com",
            new List<ItemImage>()
            {
                new ItemImage("itemName/image.png", "image/png"),
            });
        await sut.Handle(command);
        
        // Assert
        Assert.Single(wishlist.Items);
        Assert.NotEqual(wishlistUpdatedAt, wishlist.UpdatedAt);
        wishlistRepository.Verify(r => r.UpdateAsync(wishlist), Times.Once);
    }

    [Fact]
    public async Task AddItem_Should_Throw_UnauthorizedWishlistAccessException_When_Its_Anothers_Wishlist()
    {
        // Arrange
        Guid currentUserId = Guid.NewGuid();
        Guid anotherUserId = Guid.NewGuid();
        Domain.Entities.Wishlist anotherUserWishlist = new Domain.Entities.Wishlist(anotherUserId);
        
        Mock<IUser> currentUser = new Mock<IUser>();
        currentUser.Setup(u => u.Id).Returns(currentUserId);
        
        Mock<IWishlistRepository> wishlistRepository = new Mock<IWishlistRepository>();
        wishlistRepository.Setup(r => r.GetByIdAsync(anotherUserWishlist.Id)).ReturnsAsync(anotherUserWishlist);
        
        // Act
        AddItemCommandHandler sut = new AddItemCommandHandler(currentUser.Object, wishlistRepository.Object);
        AddItemCommand command = new AddItemCommand(
            anotherUserWishlist.Id, 
            "Item 1", 
            "Item description", 
            "https://www.google.com",
            new List<ItemImage>()
            {
                new ItemImage("itemName/image.png", "image/png"),
            });
        
        // Assert
        await Assert.ThrowsAsync<UnauthorizedWishlistAccessException>(() => sut.Handle(command));
    }
}