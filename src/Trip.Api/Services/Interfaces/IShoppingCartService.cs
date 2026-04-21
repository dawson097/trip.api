using Trip.Api.Dtos.Order;
using Trip.Api.Dtos.ShoppingCart;
using Trip.Api.Entities;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 购物车仓储服务
/// </summary>
public interface IShoppingCartService : ICommonService<ShoppingCart>
{
    Task<ShoppingCartDto> GetShoppingCartByUserIdAsync(string userId);

    Task<OrderDto> CheckoutAsync(string userId);
}