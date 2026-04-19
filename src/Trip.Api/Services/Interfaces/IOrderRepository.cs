using Trip.Api.Entities;

namespace Trip.Api.Services.Interfaces;

/// <summary>
/// 订单仓储服务
/// </summary>
public interface IOrderRepository : ICommonRepository
{
    /// <summary>
    /// 根据用户id获取该用户的所有订单实体数据
    /// </summary>
    /// <param name="userId">用户id</param>
    /// <returns>所有订单</returns>
    Task<IEnumerable<Order>> GetAllOrdersAsync(string userId);

    /// <summary>
    /// 根据订单id获取单个订单实体数据
    /// </summary>
    /// <param name="orderId">订单id</param>
    /// <returns>单个订单</returns>
    Task<Order> GetOrderByIdAsync(Guid orderId);
}