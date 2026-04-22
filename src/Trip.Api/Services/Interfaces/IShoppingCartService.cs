using Trip.Api.Dtos.Order;
using Trip.Api.Dtos.ShoppingCart;
using Trip.Api.Entities;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 购物车仓储服务
/// </summary>
public interface IShoppingCartService : ICommonService<ShoppingCart>
{
    /// <summary>
    /// 根据用户id获取对应的购物车
    /// </summary>
    /// <param name="userId">用户id</param>
    /// <returns>对应的购物车</returns>
    Task<ShoppingCartDto> GetShoppingCartByUserIdAsync(string userId);

    /// <summary>
    /// 根据用户id将对应购物车内的商品数据进行结算
    /// </summary>
    /// <param name="userId">用户id</param>
    /// <returns>完成支付操返回对应数据</returns>
    Task<OrderDto> CheckoutAsync(string userId);
}