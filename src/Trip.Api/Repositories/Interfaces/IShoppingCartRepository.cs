using Trip.Api.Entities;

namespace Trip.Api.Repositories.Interfaces;

/// <summary>
/// 购物车仓储服务
/// </summary>
public interface IShoppingCartRepository : ICommonRepository<ShoppingCart>
{
    /// <summary>
    /// 根据用户id从数据库中获取对应的单个购物车实体数据
    /// </summary>
    /// <param name="userId">用户id</param>
    /// <returns>对应的单个购物车</returns>
    Task<ShoppingCart> GetCartByUserIdAsync(string userId);

    /// <summary>
    /// 接收传入的购物车实体数据并插入到数据库中
    /// </summary>
    /// <param name="cart">购物车实体</param>
    Task CreateCartAsync(ShoppingCart cart);
}