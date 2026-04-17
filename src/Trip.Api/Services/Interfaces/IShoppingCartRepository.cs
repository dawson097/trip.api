using Trip.Api.Entities;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 购物车仓储服务
/// </summary>
public interface IShoppingCartRepository : ICommonRepository
{
    /// <summary>
    /// 根据用户id获取对应的单个购物车实体数据
    /// </summary>
    /// <param name="userId">用户id</param>
    /// <returns>对应的单个购物车</returns>
    Task<ShoppingCart?> GetShoppingCartById(string userId);

    /// <summary>
    /// 根据商品id获取对应的单个商品实体数据
    /// </summary>
    /// <param name="itemId">商品id</param>
    /// <returns>对应的单个商品</returns>
    Task<CartLineItem?> GetCartLineItemById(int itemId);

    /// <summary>
    /// 创建购物车实体数据
    /// </summary>
    /// <param name="shoppingCart">购物车实体</param>
    Task CreateShoppingCart(ShoppingCart shoppingCart);

    /// <summary>
    /// 创建购物车商品列表实体数据
    /// </summary>
    /// <param name="cartLineItem">购物车商品列表</param>
    Task CreateShoppingCartItem(CartLineItem cartLineItem);
}