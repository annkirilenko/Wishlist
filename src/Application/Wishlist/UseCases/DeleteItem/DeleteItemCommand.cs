namespace Wishlist.Application.Wishlist.UseCases.DeleteItem;

public class DeleteItemCommand(Guid itemId)
{
    public Guid ItemId { get; } = itemId;
}