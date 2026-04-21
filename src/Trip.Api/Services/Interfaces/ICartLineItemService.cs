using Trip.Api.Dtos.CartLineItem;
using Trip.Api.Entities;

namespace Trip.Api.Services.Interfaces;

public interface ICartLineItemService : ICommonService<CartLineItem>
{
    Task<CartLineItemDto> CreateItemAsync(string userId, CartLineItemCreateDto itemCreateDto);

    Task DeleteItemAsync(int itemId);

    Task DeleteItemsAsync(IEnumerable<int> itemIds);
}