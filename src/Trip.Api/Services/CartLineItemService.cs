using MapsterMapper;
using Trip.Api.Dtos.CartLineItem;
using Trip.Api.Entities;
using Trip.Api.Repositories.Interfaces;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class CartLineItemService(
    ICommonRepository<CartLineItem> commonRepository,
    IShoppingCartRepository cartRepository,
    ICartLineItemRepository itemRepository,
    ITouristRouteRepository routeRepository,
    IMapper mapper)
    : CommonService<CartLineItem>(commonRepository), ICartLineItemService
{
    public async Task<CartLineItemDto> CreateItemAsync(string userId, CartLineItemCreateDto itemCreateDto)
    {
        var cartFromRepo = await cartRepository.GetCartByUserIdAsync(userId);
        var routeFromRepo = await routeRepository.GetRouteByIdAsync(itemCreateDto.TouristRouteId);

        var item = new CartLineItem
        {
            TouristRouteId = itemCreateDto.TouristRouteId,
            ShoppingCartId = cartFromRepo.Id,
            OriginalPrice = routeFromRepo.OriginalPrice,
            DiscountPresent = routeFromRepo.DiscountPresent
        };

        await itemRepository.CreateItemAsync(item);
        await itemRepository.SaveAsync();

        return mapper.Map<CartLineItemDto>(item);
    }

    public async Task DeleteItemAsync(int itemId)
    {
        var item = await itemRepository.GetItemByIdAsync(itemId);

        itemRepository.DeleteItem(item);
        await itemRepository.SaveAsync();
    }

    public async Task DeleteItemsAsync(IEnumerable<int> itemIds)
    {
        var items = await itemRepository.GetItemsByIdsAsync(itemIds);

        itemRepository.DeleteItems(items);
        await itemRepository.SaveAsync();
    }
}