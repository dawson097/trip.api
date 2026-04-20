using Trip.Api.Entities;
using Trip.Api.Helpers;

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
    /// <param name="pageSize">每页显示数据条数</param>
    /// <param name="pageNumber">当前请求页面</param>
    /// <returns>所有订单</returns>
    Task<PaginationHelper<Order>> GetAllOrdersByUserIdAsync(string userId, int pageSize, int pageNumber);

    /// <summary>
    /// 根据订单id获取单个订单实体数据
    /// </summary>
    /// <param name="orderId">订单id</param>
    /// <returns>单个订单</returns>
    Task<Order> GetOrderByIdAsync(Guid orderId);
}