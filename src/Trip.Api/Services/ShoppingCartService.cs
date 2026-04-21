using MapsterMapper;
using Trip.Api.Dtos.Order;
using Trip.Api.Dtos.ShoppingCart;
using Trip.Api.Entities;
using Trip.Api.Repositories.Interfaces;
using Trip.Api.Services.Interfaces;

namespace Trip.Api.Services;

public class ShoppingCartService(
    ICommonRepository<ShoppingCart> commonRepository,
    IShoppingCartRepository cartRepository,
    IOrderRepository orderRepository,
    IMapper mapper)
    : CommonService<ShoppingCart>(commonRepository), IShoppingCartService
{
    public async Task<ShoppingCartDto> GetShoppingCartByUserIdAsync(string userId)
    {
        var shoppingCart = await cartRepository.GetCartByUserIdAsync(userId);

        return mapper.Map<ShoppingCartDto>(shoppingCart);
    }

    public async Task<OrderDto> CheckoutAsync(string userId)
    {
        var cartFromRepo = await cartRepository.GetCartByUserIdAsync(userId);

        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            OrderState = OrderState.Pending,
            OrderItems = cartFromRepo.CartLineItems,
            CreateTimeUtc = DateTime.UtcNow
        };

        cartFromRepo.CartLineItems = null;
        await orderRepository.CreateOrderAsync(order);
        await cartRepository.SaveAsync();

        return mapper.Map<OrderDto>(order);
    }
}